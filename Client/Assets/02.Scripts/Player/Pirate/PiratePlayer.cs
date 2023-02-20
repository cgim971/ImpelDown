using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class PiratePlayer : BasePlayer {

    #region Property
    public PirateInputModule PirateInputModule => _baseInputModule as PirateInputModule;
    public PirateMoveModule PirateMoveModule => _baseMoveModule as PirateMoveModule;
    public PirateCatchModule PirateCatchModule => _baseCatchModule as PirateCatchModule;
    public PirateSkillModule PirateSkillModule => _baseSkillModule as PirateSkillModule;
    #endregion

    public override void Init(bool isPlayer, int playerId, PlayerState playerState, int tailIndex, int targetTailIndex) {
        base.Init(isPlayer, playerId, playerState, tailIndex, targetTailIndex);

        AddComponents();

        InitComponent();
    }

    private void AddComponents() {
        _baseInputModule = gameObject.AddComponent<PirateInputModule>();
        _baseMoveModule = gameObject.AddComponent<PirateMoveModule>();
        _baseCatchModule = gameObject.AddComponent<PirateCatchModule>();
        _baseSkillModule = gameObject.AddComponent<PirateSkillModule>();
    }
}
