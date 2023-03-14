using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class MagicianPlayer : BasePlayer
{

    public override void Init(bool isPlayer, int playerId, PlayerState playerState)
    {
        base.Init(isPlayer, playerId, playerState);

        AddComponents();

        InitComponents();
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
