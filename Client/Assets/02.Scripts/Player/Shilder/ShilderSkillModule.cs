using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShilderSkillModule : BaseSkillModule
{
    public ShilderPlayer RobotPlayer => _player as ShilderPlayer;

    public override void Init()
    {
        _player = GetComponent<ShilderPlayer>();
    }

    public override void Skill()
    {
        if (!Input.GetKeyDown(KeyCode.Q) || !Player.InputModule.Skillable)
            return;
        Player.InputModule.Skillable = false;
        //RobotPlayer.SuperArmer = true;
        //StartCoroutine(RunTime());
        StartCoroutine(CoolTime());
    }

    //IEnumerator RunTime()
    //{
    //    //yield return new WaitForSeconds(RobotPlayer.RobotDataSO.SkillRunTime);
    //    //RobotPlayer.SuperArmer = false;
    //}
}
