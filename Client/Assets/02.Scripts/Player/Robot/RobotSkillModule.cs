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
        if (!Input.GetKeyDown(KeyCode.Q) || !_player.SkillModule.IsSkillable)
            return;
        RobotPlayer.SkillModule.IsSkillable = false;
        RobotPlayer.SuperArmer = true;
        StartCoroutine(RunTime());
        StartCoroutine(CoolTime());
    }

    IEnumerator RunTime()
    {
        yield return new WaitForSeconds(RobotPlayer.RobotDataSO.SkillRunTime);
        RobotPlayer.SuperArmer = false;
    }
}
