using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class RaccoonPlayer : BasePlayer
{
    public RaccoonPlayerDataSO RaccoonPlayerDataSO => PlayerDataSO as RaccoonPlayerDataSO;

    public override void Init(bool isPlayer, int playerId, PlayerState playerState)
    {
        base.Init(isPlayer, playerId, playerState);

        AddComponents();

        InitComponents();
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
