using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Enums;
/// <summary>
/// ������ �߻� Ŭ����
/// </summary>
public class BaseMoveModule : MonoBehaviour
{

    protected Player player;
    //�������� �����ϳ� �� �����ϳ� 
    private bool _moveable = true;
    public bool Moveable { get => _moveable; set => _moveable = value; }
    //�Է� �����ִ� �������̽� ������ Baseinput���� 
    protected IInputProvider _inputProvider;
    Vector3 _targetPos = Vector3.zero;

    protected virtual void Start()
    {
        player = GetComponent<Player>();
        _inputProvider = GetComponent<IInputProvider>();
        //�̺�Ʈ�� �߰�
        player._InputModule.OnMoveEvent.AddListener(Move);
    }

    public void Move()
    {
        //���� getaxis�� ������ ���
        var inputX = _inputProvider.GetAxis(Axis.X);
        var inputY = _inputProvider.GetAxis(Axis.Y);
        //������ �� ������ inputX, inputY = 0 ������ ó���ϸ� �ִϸ��̼��� �̻��ؼ� �ϴ� �̷��� ��
        if (_moveable == false)
            inputX = inputY = 0;
        
        Vector2 speed = new Vector2(inputX, inputY);
        Vector3 vel = new Vector3(inputX, inputY, 0).normalized;

        player.Rb.velocity = vel * player._Speed;
    }

    public void SetPositionData(Vector3 pos, bool isImmediate)
    {
        if (isImmediate)
        {
            player.transform.position = pos;
        }
        else
        {
            _targetPos = pos;
        }
    }
}
