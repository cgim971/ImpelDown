using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterItemModule : BaseItemModule {
    public HunterPlayer HunterPlayer => _player as HunterPlayer;

    public override void Init() {
        _player = GetComponent<HunterPlayer>();
    }
}
