using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMoveModule : BaseMoveModule {
    public HunterPlayer HunterPlayer => _player as HunterPlayer;

    public override void Init() {
        _player = GetComponent<HunterPlayer>();

        _rigidbody = _player.Rigidbody;
    }

    public override void Move() {
        base.Move();
    }

}
