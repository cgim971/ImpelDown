using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterPlayer : BasePlayer {

    public override void Init(bool isPlayer, int playerId, PlayerState playerState, int tailIndex) {
        base.Init(isPlayer, playerId, playerState, tailIndex);

        AddComponents();

        InitComponents();
    }

    private void AddComponents() {
        _baseInputModule = gameObject.AddComponent<HunterInputModule>();
        _baseMoveModule = gameObject.AddComponent<HunterMoveModule>();
        _baseCatchModule = gameObject.AddComponent<HunterCatchModule>();
        _baseSkillModule = gameObject.AddComponent<HunterSkillModule>();
        _baseTailModule = gameObject.AddComponent<HunterTailModule>();
        _baseItemModule = gameObject.AddComponent<HunterItemModule>();
    }

}
