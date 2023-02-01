using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ai랑 플레이어 둘다 상속받을 추상 클래스 
/// </summary>
public abstract class Entity : MonoBehaviour
{
    //지금 이름이 잘못된 SO 사실 EntityDataSO라고 생각하면 편함
    [SerializeField]
    private BasePlayerDataSO _playerDataSO;
    public BasePlayerDataSO _PlayerDataSO => _playerDataSO;
    //SO에서 이동속도 받아놓기 이유는 나중에 속도 바꿔주는 스킬들 있는데 SO를 바꿀 수 없어서 이렇게 받아놓음
    public float p_Speed;
    //꼬리 종류 나중에 dontdestroyed로 선택한 거 저장하고 불러올 생각으로 이렇게 함
    public TailState _tailName = TailState.None;

    protected GameObject _tail = null;

    protected virtual void Awake()
    {
        SetTail();
        p_Speed = _playerDataSO.speed;
    }
    //꼬리 동적 생성 리소스에서 꺼내주는 방식으로 구현함
    public void SetTail()
    {
        switch (_tailName)
        {
            case TailState.Carret:
                GameObject temp = Resources.Load("Prefabs/Tails/CarretTail") as GameObject;
                _tail = Instantiate(temp) as GameObject;
                _tail.name = "Tail";
                _tail.transform.parent = transform;
                _tail.transform.localPosition = new Vector3(-0.5f, -0.5f, 0f);
                break;
        }
    }
}
