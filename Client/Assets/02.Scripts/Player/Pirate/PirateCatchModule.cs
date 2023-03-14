using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCatchModule : BaseCatchModule {
    public PiratePlayer PiratePlayer => _player as PiratePlayer;

    public override void Init() {
        _player = GetComponent<PiratePlayer>();
        base.Init();
    }
}
