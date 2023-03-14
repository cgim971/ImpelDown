using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaMoveModule : BaseMoveModule
{
    public NinjaPlayer NinjaPlayer => _player as NinjaPlayer;

    public override void Init()
    {
        _player = GetComponent<NinjaPlayer>();

        _rigidbody = _player.Rigidbody;
    }

    public override void Move()
    {
        base.Move();
    }
}
