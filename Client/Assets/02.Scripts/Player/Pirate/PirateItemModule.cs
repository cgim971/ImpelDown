using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateItemModule : BaseItemModule {
    public PiratePlayer PiratePlayer => _player as PiratePlayer;

    public override void Init() {
        _player = GetComponent<PiratePlayer>();
    }
}
