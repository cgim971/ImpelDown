using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShilderCatchModule : BaseCatchModule
{
    public ShilderPlayer RobotPlayer => _player as ShilderPlayer;

    public override void Init()
    {
        _player = GetComponent<ShilderPlayer>();
        base.Init();
    }
}
