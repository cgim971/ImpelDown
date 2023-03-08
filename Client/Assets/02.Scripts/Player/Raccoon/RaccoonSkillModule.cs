using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonSkillModule : BaseSkillModule
{
    public RaccoonPlayer PiratePlayer => _player as RaccoonPlayer;

    public override void Init()
    {
        _player = GetComponent<RaccoonPlayer>();
    }

    public override void Skill()
    {
        if (Skillable() == false)
            return;

    }
}
