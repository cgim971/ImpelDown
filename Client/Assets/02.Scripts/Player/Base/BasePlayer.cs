using Cinemachine;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasePlayer : MonoBehaviour {
    #region Property
    public BaseInputModule InputModule => _baseInputModule;
    public BaseMoveModule MoveModule => _baseMoveModule;
    public BaseCatchModule CatchModule => _baseCatchModule;
    public BaseSkillModule SkillModule => _baseSkillModule;
    public BaseTailModule TailModule => _baseTailModule;
    public BaseItemModule ItemModule => _baseItemModule;

    public BasePlayerDataSO PlayerDataSO => _basePlayerDataSO;

    public Rigidbody2D Rigidbody => _rigidbody;

    public bool IsPlayer => _isPlayer;
    public int PlayerId => _playerId;
    public PlayerState PlayerState => _playerState;
    #endregion

    protected BaseInputModule _baseInputModule;
    protected BaseMoveModule _baseMoveModule;
    protected BaseCatchModule _baseCatchModule;
    protected BaseSkillModule _baseSkillModule;
    protected BaseTailModule _baseTailModule;
    protected BaseItemModule _baseItemModule;

    [SerializeField] protected BasePlayerDataSO _basePlayerDataSO;

    protected Rigidbody2D _rigidbody;
    private Transform _agentRendererTs;
    private Transform _agentGhostRendererTs;


    protected bool _isPlayer = false;
    protected int _playerId = -1;


    private float _width = 0;
    CinemachineVirtualCamera vCam = null;

    private PlayerState _playerState = PlayerState.PlayerNone;

    private void Awake() {
        _width = Screen.width / 2;
        _agentRendererTs = transform.Find("AgentRenderer");
        _agentGhostRendererTs = transform.Find("AgentGhostRenderer");
    }

    public virtual void Init(bool isPlayer, int playerId, PlayerState playerState) {
        _isPlayer = isPlayer;
        _playerId = playerId;

        SetPlayerState(playerState);

        _rigidbody = GetComponent<Rigidbody2D>();

        if (isPlayer) {
            PlayerManager.Instance.RemotePlayer = this;

            vCam = GameObject.Find("FollowCam").GetComponent<CinemachineVirtualCamera>();
            if (vCam != null) {
                vCam.m_Follow = this.transform;
            }
        }
    }

    protected void InitComponents() {
        _baseInputModule.Init();
        _baseMoveModule.Init();
        _baseCatchModule.Init();
        _baseSkillModule.Init();
        _baseItemModule.Init();
        _baseTailModule.Init();
    }

    protected virtual void Start() {
        if (_isPlayer == true) {
            StartCoroutine(SendPosition());
        }
    }

    protected virtual void Update() {
        if (_isPlayer == true) {
            _baseInputModule.InputCatch();
            _baseInputModule.InputSkill();
            LookMouse();
        }
    }

    protected virtual void FixedUpdate() {
        if (_isPlayer) {
            _baseInputModule.InputMove();
        }
    }

    public void LookMouse() {
        float x = Input.mousePosition.x;
        int scaleX = (int)(x == _width ? transform.localScale.x : x > _width ? 1 : -1);

        transform.localScale = new Vector3(scaleX, 1, 1);
    }

    public void SetPositionInfo(PositionData positionData, bool isImmediate = false) {
        _baseMoveModule.SetPositionData(positionData.pos, isImmediate);
        transform.localScale = new Vector3(positionData.scaleX, 1, 1);
    }


    public void SetPlayerState(PlayerState playerState) {
        _playerState = playerState;

        switch (_playerState) {
            case PlayerState.PlayerNone:
                break;
            case PlayerState.Alive:
                if (IsPlayer)
                    Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << LayerMask.NameToLayer("GhostTs"));

                SetActiveTs(_agentRendererTs);
                SetActiveTs(_agentGhostRendererTs, false);
                break;
            case PlayerState.Catched:
                break;
            case PlayerState.Ghost:
                if (IsPlayer)
                    Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("GhostTs");

                // 유령일 때
                SetActiveTs(_agentRendererTs, false);
                SetActiveTs(_agentGhostRendererTs);


                break;
        }
    }

    void SetActiveTs(Transform ts, bool isActive = true) => ts.gameObject.SetActive(isActive);

    protected virtual IEnumerator SendPosition() {
        PlayerPosData playerPosData = new PlayerPosData {
            Position = new Position(),
            ScaleX = transform.localScale.x
        };


        while (gameObject.activeSelf) {
            yield return new WaitForSeconds(0.04f);

            Vector2 pos = transform.position;

            playerPosData.Position.X = pos.x;
            playerPosData.Position.Y = pos.y;

            playerPosData.ScaleX = _agentRendererTs.localScale.x;

            C_Move cMove = new C_Move { PlayerId = PlayerId, PlayerPosData = playerPosData };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CMove, cMove);
        }
    }
}
