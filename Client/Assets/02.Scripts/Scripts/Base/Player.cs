using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected Animator _animator;
    public Animator Animator => _animator;

    protected Rigidbody2D _rb;
    public Rigidbody2D Rb => _rb;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _inputModule = GetComponent<BaseInputModule>();
        _moveModule = GetComponent<BaseMoveModule>();
        _skillModule = GetComponent<BaseSkillModule>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        
    }
}
