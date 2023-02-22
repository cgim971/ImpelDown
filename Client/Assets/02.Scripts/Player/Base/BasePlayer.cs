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

    public BasePlayerDataSO PlayerDataSO => _basePlayerDataSO;

    public Rigidbody2D Rigidbody => _rigidbody;

    public bool IsPlayer => _isPlayer;
    public int PlayerId => _playerId;
    public int TailIndex => _tailIndex;
    #endregion

    protected BaseInputModule _baseInputModule;
    protected BaseMoveModule _baseMoveModule;
    protected BaseCatchModule _baseCatchModule;
    protected BaseSkillModule _baseSkillModule;

    [SerializeField] private BasePlayerDataSO _basePlayerDataSO;

    protected Rigidbody2D _rigidbody;
    private Transform _agentRendererTs;


    protected bool _isPlayer = false;
    protected int _playerId = -1;

    protected int _tailIndex = -1;


    private float _width = 0;


    private void Awake() {
        _width = Screen.width / 2;
        _agentRendererTs = transform.Find("AgentRenderer");
        Init(true, 0, 0);
    }

    public virtual void Init(bool isPlayer, int playerId, int tailIndex) {
        _isPlayer = isPlayer;
        _playerId = playerId;
        _tailIndex = tailIndex;

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void InitComponent() {
        _baseInputModule.Init();
        _baseMoveModule.Init();
        _baseCatchModule.Init();
        _baseSkillModule.Init();
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

        _agentRendererTs.localScale = new Vector3(scaleX, 1, 1);
    }

    public void SetPositionInfo(PositionData positionData, bool isImmediate = false) {
        _baseMoveModule.SetPositionData(positionData.pos, isImmediate);
        _agentRendererTs.localScale = new Vector3(positionData.scaleX, 1, 1);
    }

    protected virtual IEnumerator SendPosition() {
        PositionInfo positionInfo = new PositionInfo {
            Position = new Position(),
            ScaleX = _agentRendererTs.localScale.x
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
