using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaPlayer : Player
{
    public NinjaInputModule InputModule => _inputModule as NinjaInputModule;
    public NinjaSkillModule SkillModule => _skillModule as NinjaSkillModule;
    public NinjaMoveModule MoveModule => _moveModule as NinjaMoveModule;
    public NinjaDataSO PlayerDataSO => _PlayerDataSO as NinjaDataSO;

    protected override void Awake()
    {
        base.Awake();
    }
}
