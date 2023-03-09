using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSkillModule : BaseSkillModule
{
    public RobotPlayer RobotPlayer => _player as RobotPlayer;

    public override void Init()
    {
        _player = GetComponent<RobotPlayer>();
    }

    public override void Skill()
    {
        if (Skillable() == false)
            return;

    }
}
