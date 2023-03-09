using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotInputModule : BaseInputModule
{
    public RobotPlayer RobotPlayer => _player as RobotPlayer;

    public override void Init()
    {
        _player = GetComponent<RobotPlayer>();
    }
}
