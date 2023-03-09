using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianSkillModule : BaseSkillModule
{
    public MagicianPlayer MagicianPlayer => _player as MagicianPlayer;

    public override void Init()
    {
        _player = GetComponent<MagicianPlayer>();
    }

    public override void Skill()
    {
        if (Skillable() == false)
            return;

    }
}
