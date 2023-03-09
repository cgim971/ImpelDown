using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class RaccoonPlayer : BasePlayer
{
    #region Property
    public RaccoonInputModule RaccoonInputModule => _baseInputModule as RaccoonInputModule;
    public RaccoonMoveModule RaccoonMoveModule => _baseMoveModule as RaccoonMoveModule;
    public RaccoonCatchModule RaccoonCatchModule => _baseCatchModule as RaccoonCatchModule;
    public RaccoonSkillModule RaccoonSkillModule => _baseSkillModule as RaccoonSkillModule;
    public RaccoonItemModule RaccoonItemModule => _baseItemModule as RaccoonItemModule;
    public RaccoonTailMoudule RaccoonTailModule => _baseTailModule as RaccoonTailMoudule;
    public RaccoonDataSO RaccoonDataSO => _basePlayerDataSO as RaccoonDataSO;
    #endregion

    public override void Init(bool isPlayer, int playerId, PlayerState playerState, int tailIndex, int targetTailIndex)
    {
        base.Init(isPlayer, playerId, playerState, tailIndex, targetTailIndex);

        AddComponents();

        InitComponent();
    }

    private void AddComponents()
    {
        _baseInputModule = gameObject.AddComponent<RaccoonInputModule>();
        _baseMoveModule = gameObject.AddComponent<RaccoonMoveModule>();
        _baseCatchModule = gameObject.AddComponent<RaccoonCatchModule>();
        _baseSkillModule = gameObject.AddComponent<RaccoonSkillModule> ();
        _baseItemModule = gameObject.AddComponent<RaccoonItemModule>();
        _baseTailModule = gameObject.AddComponent<RaccoonTailMoudule>();
    }
}
