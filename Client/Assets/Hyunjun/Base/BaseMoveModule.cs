using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Enums;
/// <summary>
/// 움직임 추상 클래스
/// </summary>
public class BaseMoveModule : MonoBehaviour
{

    protected Player player;
    //움직임이 가능하나 안 가능하나 
    private bool _moveable = true;
    public bool Moveable { get => _moveable; set => _moveable = value; }
    //입력 도와주는 인터페이스 구현은 Baseinput에서 
    protected IInputProvider _inputProvider;
    Vector3 _targetPos = Vector3.zero;

    protected virtual void Start()
    {
        player = GetComponent<Player>();
        _inputProvider = GetComponent<IInputProvider>();
        //이벤트에 추가
        player._InputModule.OnMoveEvent.AddListener(Move);
    }

    public void Move()
    {
        //기존 getaxis와 동일한 기능
        var inputX = _inputProvider.GetAxis(Axis.X);
        var inputY = _inputProvider.GetAxis(Axis.Y);
        //움직일 수 없으면 inputX, inputY = 0 위에서 처리하면 애니메이션이 이상해서 일단 이렇게 함
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
