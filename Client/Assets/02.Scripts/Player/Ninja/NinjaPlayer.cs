using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaPlayer : BasePlayer
{
    #region Property
    public NinjaInputModule NinjaInputModule => _baseInputModule as NinjaInputModule;
    public NinjaMoveModule NinjaMoveModule => _baseMoveModule as NinjaMoveModule;
    public NinjaCatchModule NinjaCatchModule => _baseCatchModule as NinjaCatchModule;
    public NinjaSkillModule NinjaSkillModule => _baseSkillModule as NinjaSkillModule;
    #endregion

    public override void Init(bool isPlayer, int playerId, int tailIndex)
    {
        base.Init(isPlayer, playerId, tailIndex);

        AddComponents();

        InitComponent();
    }

    private void AddComponents()
    {
        Debug.Log("1");
        _baseInputModule = gameObject.AddComponent<NinjaInputModule>();
        _baseMoveModule = gameObject.AddComponent<NinjaMoveModule>();
        _baseCatchModule = gameObject.AddComponent<NinjaCatchModule>();
        _baseSkillModule = gameObject.AddComponent<NinjaSkillModule>();
    }
}
