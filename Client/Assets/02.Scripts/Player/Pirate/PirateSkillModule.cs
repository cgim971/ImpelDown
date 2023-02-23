using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSkillModule : BaseSkillModule {
    public PiratePlayer PiratePlayer => _player as PiratePlayer;

    public override void Init() {
        _player = GetComponent<PiratePlayer>();
    }

    public override void Skill() {
        if (Skillable() == false)
            return;

    }
}
