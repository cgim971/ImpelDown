using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ai�� �÷��̾� �Ѵ� ��ӹ��� �߻� Ŭ���� 
/// </summary>
public abstract class Entity : MonoBehaviour
{
    #region Property
    public bool IsPlayer => _isPlayer;
    #endregion

    protected bool _isPlayer = false;


    [SerializeField]
    protected BaseEntityDataSO _entityDataSO;
    
    protected float _speed;
    public float _Speed => _speed;

    protected virtual void Awake()
    {
        _speed = _entityDataSO.speed;
    }
}
