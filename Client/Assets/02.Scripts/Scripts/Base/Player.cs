using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 추상 클래스 
/// </summary>
public abstract class Player : Entity
{
    //모든 Base들을 여기서 관리 그래서 나중에 각각의 모듈들이 서로 연결할 때 플레이어만 거쳐가면 됨 그리고 각종 rigidbody도 여기서 관리
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
