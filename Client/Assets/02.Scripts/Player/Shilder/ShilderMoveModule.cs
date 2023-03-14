using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShilderMoveModule : BaseMoveModule
{
    public ShilderPlayer RobotPlayer => _player as ShilderPlayer;

    public override void Init()
    {
        _player = GetComponent<ShilderPlayer>();
        _rigidbody = _player.Rigidbody;
    }

    public override void Move()
    {
        base.Move();
    }
}
