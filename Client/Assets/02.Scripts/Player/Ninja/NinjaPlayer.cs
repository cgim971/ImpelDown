using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaPlayer : BasePlayer {
    #region Property
    public NinjaInputModule NinjaInputModule => _baseInputModule as NinjaInputModule;
    public NinjaMoveModule NinjaMoveModule => _baseMoveModule as NinjaMoveModule;
    public NinjaCatchModule NinjaCatchModule => _baseCatchModule as NinjaCatchModule;
    public NinjaSkillModule NinjaSkillModule => _baseSkillModule as NinjaSkillModule;
    public NinjaTailModule NinjaTailModule => _baseTailModule as NinjaTailModule;
    public NinjaItemModule NinjaItemModule => _baseItemModule as NinjaItemModule;
    public NinjaDataSO NinjaDataSO => _basePlayerDataSO as NinjaDataSO;
    #endregion

    public override void Init(bool isPlayer, int playerId, PlayerState playerState, int tailIndex, int targetTailIndex) {
        base.Init(isPlayer, playerId, playerState, tailIndex, targetTailIndex);


        AddComponents();

        InitComponent();
    }

    private void AddComponents() {
        _baseInputModule = gameObject.AddComponent<NinjaInputModule>();
        _baseMoveModule = gameObject.AddComponent<NinjaMoveModule>();
        _baseCatchModule = gameObject.AddComponent<NinjaCatchModule>();
        _baseSkillModule = gameObject.AddComponent<NinjaSkillModule>();
        _baseTailModule = gameObject.AddComponent<NinjaTailModule>();
        _baseItemModule = gameObject.AddComponent<NinjaItemModule>();
    }
}
