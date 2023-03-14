using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianMoveModule : BaseMoveModule
{
    public MagicianPlayer NinjaPlayer => _player as MagicianPlayer;

    public override void Init()
    {
        _player = GetComponent<MagicianPlayer>();

        _rigidbody = _player.Rigidbody;
    }

    public override void Move()
    {
        base.Move();
    }
}
