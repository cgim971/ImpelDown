using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class MagicianPlayer : BasePlayer
{
    #region Property
    public MagicianInputModule MagicianInputModule => _baseInputModule as MagicianInputModule;
    public MagicianMoveModule MagicianMoveModule => _baseMoveModule as MagicianMoveModule;
    public MagicianCatchModule MagicianCatchModule => _baseCatchModule as MagicianCatchModule;
    public MagicianSkillModule MagicianSkillModule => _baseSkillModule as MagicianSkillModule;
    public MagicianItemModule MagicianItemModule => _baseItemModule as MagicianItemModule;
    public MagicianTailModule MagicianTailModule => _baseTailModule as MagicianTailModule;
    public MagicianDataSO MagicianDataSO => _basePlayerDataSO as MagicianDataSO;
    #endregion

    public override void Init(bool isPlayer, int playerId, PlayerState playerState, int tailIndex, int targetTailIndex)
    {
        base.Init(isPlayer, playerId, playerState, tailIndex, targetTailIndex);

        AddComponents();

        InitComponent();
    }

    private void AddComponents()
    {
        _baseInputModule = gameObject.AddComponent<MagicianInputModule>();
        _baseMoveModule = gameObject.AddComponent<MagicianMoveModule>();
        _baseCatchModule = gameObject.AddComponent<MagicianCatchModule>();
        _baseSkillModule = gameObject.AddComponent<MagicianSkillModule>();
        _baseItemModule = gameObject.AddComponent<MagicianItemModule>();
        _baseTailModule = gameObject.AddComponent<MagicianTailModule>();
    }
}
