using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterTailModule : BaseTailModule {
    public HunterPlayer HunterPlayer => _player as HunterPlayer;

    public override void Init() {
        _player = GetComponent<HunterPlayer>();
        base.Init();
    }
}
