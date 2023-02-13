using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Interfaces;
using ImpelDown.Proto;
/// <summary>
/// �÷��̾� �߻� Ŭ���� 
/// </summary>
public abstract class Player : Entity
{
    //��� Base���� ���⼭ ���� �׷��� ���߿� ������ ������ ���� ������ �� �÷��̾ ���İ��� �� �׸��� ���� rigidbody�� ���⼭ ����
    protected BaseInputModule _inputModule;
    public BaseInputModule _InputModule => _inputModule;

    protected BaseMoveModule _moveModule;
    public BaseMoveModule _MoveModule => _moveModule;

    protected BaseSkillModule _skillModule;
    public BaseSkillModule _SkillModule => _skillModule;

    protected BaseCatchModule _catchModule;
    public BaseCatchModule _CatchModule => _catchModule;

    public BasePlayerDataSO _PlayerDataSO => _entityDataSO as BasePlayerDataSO;

    protected Animator _animator;
    public Animator Animator => _animator;

    public int PlayerId => _playerId;
    private int _playerId;

    protected Rigidbody2D _rb;
    public Rigidbody2D Rb => _rb;

    protected override void Awake()
    {
        base.Awake();
        Init(true, 0);
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

        if (_isPlayer == true)
        {
            //StartCoroutine(SendPositionAndRotation());
        }
    }

    public void Start()
    {
        if (_isPlayer == true)
        {
            //_catchModule.SetTargetId(PlayerManager.Instance.GetTargetId(TailIndex));
        }
    }


    private void Update()
    {
        if (_isPlayer == true)
        {
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
