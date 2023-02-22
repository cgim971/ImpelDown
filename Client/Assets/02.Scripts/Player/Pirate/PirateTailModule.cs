using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateTailModule : BaseTailModule {
    public PiratePlayer PiratePlayer => _player as PiratePlayer;

    public override void Init() {
        _player = GetComponent<PiratePlayer>();
    }
}
