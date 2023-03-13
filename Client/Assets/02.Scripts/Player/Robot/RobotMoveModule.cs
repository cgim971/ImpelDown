using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMoveModule : BaseMoveModule
{
    public RobotPlayer RobotPlayer => _player as RobotPlayer;

    public override void Init()
    {
        _player = GetComponent<RobotPlayer>();
        _rigidbody = _player.Rigidbody;
        base.Init();
    }

    public override void Move()
    {
        base.Move();
    }
}
