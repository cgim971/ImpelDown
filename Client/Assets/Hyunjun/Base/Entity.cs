using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ai랑 플레이어 둘다 상속받을 추상 클래스 
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
