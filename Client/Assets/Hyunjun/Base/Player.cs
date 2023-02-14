using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;
using ImpelDown.Proto;
/// <summary>
/// 플레이어 추상 클래스 
/// </summary>
public abstract class Player : Entity
{
    //모든 Base들을 여기서 관리 그래서 나중에 각각의 모듈들이 서로 연결할 때 플레이어만 거쳐가면 됨 그리고 각종 rigidbody도 여기서 관리
    protected BaseInputModule _inputModule;
    public BaseInputModule InputModule => _inputModule;

    protected BaseMoveModule _moveModule;
    public BaseMoveModule MoveModule => _moveModule;

    protected BaseSkillModule _skillModule;
    public BaseSkillModule SkillModule => _skillModule;

    protected BaseCatchModule _catchModule;
    public BaseCatchModule CatchModule => _catchModule;

    public BasePlayerDataSO PlayerDataSO => _entityDataSO as BasePlayerDataSO;

    protected Animator _animator;
    public Animator Animator => _animator;

    public int PlayerId => _playerId;
    private int _playerId;

    protected Rigidbody2D _rb;
    public Rigidbody2D Rb => _rb;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _inputModule = GetComponent<BaseInputModule>();
        _moveModule = GetComponent<BaseMoveModule>();
        _skillModule = GetComponent<BaseSkillModule>();
        _catchModule = GetComponent<BaseCatchModule>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void Init(bool isPlayer, int playerId)
    {
        _isPlayer = isPlayer;
        _playerId = playerId;

        _moveModule.Init(this);

        if (_isPlayer == true)
        {
            //StartCoroutine(SendPositionAndRotation());
        }
    }

    private void Update()
    {
        if (_isPlayer == true)
        {
            InputModule.PlayerUpdate();
            // PlayAnimation();
        }
    }

    public void PlayAnimation()
    {

    }

    public void SetAnimation()
    {

    }

    //public void SetPositionData(PositionData positionData, bool isImmediate = false)
    //{
    //    _moveModule.SetPositionData(positionData.pos, isImmediate);
    //}

    //public void SetTailColor(int tailNo)
    //{
    //    //_tailController.SetTail(tailNo);
    //}

    //private IEnumerator SendPositionAndRotation()
    //{
    //    PosAndRot posAndRot = new PosAndRot();

    //    while (gameObject.activeSelf)
    //    {
    //        yield return new WaitForSeconds(0.04f);

    //        Vector2 pos = transform.position;

    //        posAndRot.X = pos.x;
    //        posAndRot.Y = pos.y;

    //        PlayerAllData playerAllData = new PlayerAllData { PlayerData = new PlayerData { PlayerId = _playerId }, PosAndRot = posAndRot };
    //        C_Move cMove = new C_Move { PlayerAllData = playerAllData };

    //        NetworkManager.Instance.RegisterSend((ushort)MSGID.CMove, cMove);
    //    }
    //}
}
