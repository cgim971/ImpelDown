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
    public TailSO TailSO => _tailSO;

    public Rigidbody2D Rigidbody => _rigidbody;

    public bool IsPlayer => _isPlayer;
    public int PlayerId => _playerId;
    public int TailIndex => _tailIndex;
    public int TargetTailIndex => _targetTailIndex;
    public PlayerState PlayerState => _playerState;
    #endregion

    protected BaseInputModule _baseInputModule;
    protected BaseMoveModule _baseMoveModule;
    protected BaseCatchModule _baseCatchModule;
    protected BaseSkillModule _baseSkillModule;
    protected BaseTailModule _baseTailModule;
    protected BaseItemModule _baseItemModule;

    [SerializeField] protected BasePlayerDataSO _basePlayerDataSO;
    [SerializeField] protected TailSO _tailSO;

    protected Rigidbody2D _rigidbody;
    private Transform _agentRendererTs;
    private Transform _agentGhostRendererTs;


    protected bool _isPlayer = false;
    protected int _playerId = -1;
    protected PlayerState _playerState = PlayerState.None;

    protected int _tailIndex = -1;
    protected int _targetTailIndex = -1;


    private float _width = 0;
    CinemachineVirtualCamera vCam = null;


    private void Awake() {
        _width = Screen.width / 2;
        _agentRendererTs = transform.Find("AgentRenderer");
        _agentGhostRendererTs = transform.Find("AgentGhostRenderer");
    }

    public virtual void Init(bool isPlayer, int playerId, PlayerState playerState, int tailIndex, int targetTailIndex) {
        _isPlayer = isPlayer;
        _playerId = playerId;
        _tailIndex = tailIndex;

        SetPlayerState(playerState);
        _targetTailIndex = targetTailIndex;



        _rigidbody = GetComponent<Rigidbody2D>();

        if (isPlayer) {
            PlayerManager.Instance.RemotePlayer = this;

            vCam = GameObject.Find("FollowCam").GetComponent<CinemachineVirtualCamera>();
            if (vCam != null) {
                vCam.m_Follow = this.transform;
            }
        }
    }

    protected void InitComponent() {
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

    public void SetTargetTailIndex(int targetTailIndex)
    {
        // 꼬리 추가
        Dictionary<ETailName, int> tails = PlayerManager.Instance.GetPlayerTails(targetTailIndex);
        foreach(KeyValuePair<ETailName,int> tail in tails)
        {
            _baseTailModule.CreateTail(tail.Key,tail.Value);
        }

        _targetTailIndex = targetTailIndex;
    }

    public void SetPlayerState(PlayerState playerState) {
        _playerState = playerState;

        switch (_playerState) {
            case PlayerState.None:
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
        PositionInfo positionInfo = new PositionInfo {
            Position = new Position(),
            ScaleX = transform.localScale.x
        };

        while (gameObject.activeSelf) {
            yield return new WaitForSeconds(0.04f);

            Vector2 pos = transform.position;

            positionInfo.Position.X = pos.x;
            positionInfo.Position.Y = pos.y;

            positionInfo.ScaleX = _agentRendererTs.localScale.x;

            C_Move cMove = new C_Move { PlayerId = PlayerId, PositionInfo = positionInfo };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CMove, cMove);
        }
    }
}
