using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaPlayer : BasePlayer {

    public NinjaPlayerDataSO NinjaPlayerDataSO => PlayerDataSO as NinjaPlayerDataSO;

    public override void Init(bool isPlayer, int playerId, PlayerState playerState)
    {
        base.Init(isPlayer, playerId, playerState);

        AddComponents();

        InitComponents();
    }

    private void AddComponents() {
        _baseInputModule = gameObject.AddComponent<NinjaInputModule>();
        _baseMoveModule = gameObject.AddComponent<NinjaMoveModule>();
        _baseCatchModule = gameObject.AddComponent<NinjaCatchModule>();
        _baseSkillModule = gameObject.AddComponent<NinjaSkillModule>();
        _baseItemModule = gameObject.AddComponent<NinjaItemModule>();
        _baseTailModule = gameObject.AddComponent<NinjaTailModule>();
    }
}
