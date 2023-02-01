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
        //�ִϸ��̼� ó�� --�ٵ� ���� �ٲ㵵 �� �ִϸ��̼��� �׳� �� ������� �ѰŶ�
        player.Animator.SetFloat("Horizontal", inputX);
        player.Animator.SetFloat("Vertical", inputY);
        player.Animator.SetFloat("Speed", speed.sqrMagnitude);

        player.Rb.velocity = vel * player.p_Speed;
    }
}
