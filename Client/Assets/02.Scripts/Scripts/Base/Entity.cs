using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ai�� �÷��̾� �Ѵ� ��ӹ��� �߻� Ŭ���� 
/// </summary>
public abstract class Entity : MonoBehaviour
{
    //���� �̸��� �߸��� SO ��� EntityDataSO��� �����ϸ� ����
    [SerializeField]
    private BasePlayerDataSO _playerDataSO;
    public BasePlayerDataSO _PlayerDataSO => _playerDataSO;
    //SO���� �̵��ӵ� �޾Ƴ��� ������ ���߿� �ӵ� �ٲ��ִ� ��ų�� �ִµ� SO�� �ٲ� �� ��� �̷��� �޾Ƴ���
    public float p_Speed;
    //���� ���� ���߿� dontdestroyed�� ������ �� �����ϰ� �ҷ��� �������� �̷��� ��
    public TailState _tailName = TailState.None;

    protected GameObject _tail = null;

    protected virtual void Awake()
    {
        SetTail();
        p_Speed = _playerDataSO.speed;
    }
    //���� ���� ���� ���ҽ����� �����ִ� ������� ������
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
