using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMoveModule : BaseMoveModule {
    public PiratePlayer PiratePlayer => _player as PiratePlayer;

    public override void Init() {
        _player = GetComponent<PiratePlayer>();
        _rigidbody = _player.Rigidbody;
        base.Init();
    }

    public override void Move() {
        base.Move();
    }

}
