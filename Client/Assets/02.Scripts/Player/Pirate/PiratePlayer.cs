using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class PiratePlayer : BasePlayer {

    public override void Init(bool isPlayer, int playerId, PlayerState playerState)
    {
        base.Init(isPlayer, playerId, playerState);

        AddComponents();

        InitComponents();
    }

    private void AddComponents() {
        _baseInputModule = gameObject.AddComponent<PirateInputModule>();
        _baseMoveModule = gameObject.AddComponent<PirateMoveModule>();
        _baseCatchModule = gameObject.AddComponent<PirateCatchModule>();
        _baseSkillModule = gameObject.AddComponent<PirateSkillModule>();
        _baseItemModule = gameObject.AddComponent<PirateItemModule>();
        _baseTailModule = gameObject.AddComponent<PirateTailModule>();
    }
}
