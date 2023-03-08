using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSkillModule : BaseSkillModule {
    public HunterPlayer HunterPlayer => _player as HunterPlayer;

    public override void Init() {
        _player = GetComponent<HunterPlayer>();
    }

    public override void Skill() {
        if (Skillable() == false)
            return;

    }
}
