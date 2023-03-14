using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class ShilderPlayer : BasePlayer
{
    public ShilderPlayerDataSO RobotPlayerDataSO => PlayerDataSO as ShilderPlayerDataSO;

    public override void Init(bool isPlayer, int playerId, PlayerState playerState)
    {
        base.Init(isPlayer, playerId, playerState);

        AddComponents();

        InitComponents();
    }

    private void AddComponents()
    {
        _baseInputModule = gameObject.AddComponent<ShilderInputModule>();
        _baseMoveModule = gameObject.AddComponent<ShilderMoveModule>();
        _baseCatchModule = gameObject.AddComponent<ShilderCatchModule>();
        _baseSkillModule = gameObject.AddComponent<ShilderSkillModule>();
        _baseItemModule = gameObject.AddComponent<ShilderItemModule>();
        _baseTailModule = gameObject.AddComponent<ShilderTailModule>();
    }
}
