using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class HunterPlayer : BasePlayer {

    #region Property
    public HunterInputModule HunterInputModule => _baseInputModule as HunterInputModule;
    public HunterMoveModule HunterMoveModule => _baseMoveModule as HunterMoveModule;
    public HunterCatchModule HunterCatchModule => _baseCatchModule as HunterCatchModule;
    public HunterSkillModule HunterSkillModule => _baseSkillModule as HunterSkillModule;
    public HunterItemModule HunterItemModule => _baseItemModule as HunterItemModule;
    public HunterDataSO HunterDataSO => _basePlayerDataSO as HunterDataSO;
    #endregion

    public override void Init(bool isPlayer, int playerId, PlayerState playerState, int tailIndex, int targetTailIndex) {
        base.Init(isPlayer, playerId, playerState, tailIndex, targetTailIndex);

        AddComponents();

        InitComponent();
    }

    private void AddComponents() {
        _baseInputModule = gameObject.AddComponent<HunterInputModule>();
        _baseMoveModule = gameObject.AddComponent<HunterMoveModule>();
        _baseCatchModule = gameObject.AddComponent<HunterCatchModule>();
        _baseSkillModule = gameObject.AddComponent<HunterSkillModule>();
        _baseItemModule = gameObject.AddComponent<HunterItemModule>();
        _baseTailModule = gameObject.AddComponent<HunterTailModule>();
    }
}
