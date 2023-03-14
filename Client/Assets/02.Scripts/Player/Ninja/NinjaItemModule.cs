using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaItemModule : BaseItemModule {
    public NinjaPlayer NinjaPlayer => _player as NinjaPlayer;

    public override void Init() {
        _player = GetComponent<NinjaPlayer>();
        base.Init();
    }
}
