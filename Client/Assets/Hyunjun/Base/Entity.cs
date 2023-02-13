using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ai�� �÷��̾� �Ѵ� ��ӹ��� �߻� Ŭ���� 
/// </summary>
public abstract class Entity : MonoBehaviour
{
    protected bool _isPlayer = false;
    public bool IsPlayer => _isPlayer;


    [SerializeField]
    protected BaseEntityDataSO _entityDataSO;
    
    protected float _speed;
    public float _Speed => _speed;

    protected virtual void Awake()
    {
        _speed = _entityDataSO.speed;
    }
}
