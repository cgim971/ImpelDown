using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonMoveModule : BaseMoveModule
{
    public RaccoonPlayer RaccoonPlayer => _player as RaccoonPlayer;

    public override void Init()
    {
        _player = GetComponent<RaccoonPlayer>();
        _rigidbody = _player.Rigidbody;
        base.Init();
    }

    public override void Move()
    {
        base.Move();
    }
}
