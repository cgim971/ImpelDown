using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTailModule : BaseTailModule
{
    public RobotPlayer RobotPlayer => _player as RobotPlayer;

    public override void Init()
    {
        _player = GetComponent<RobotPlayer>();

        base.Init();
    }
}
