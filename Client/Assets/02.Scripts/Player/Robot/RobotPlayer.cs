using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class RobotPlayer : BasePlayer
{
    #region Property
    public RobotInputModule RobotInputModule => _baseInputModule as RobotInputModule;
    public RobotMoveModule RobotMoveModule => _baseMoveModule as RobotMoveModule;
    public RobotCatchModule RobotCatchModule => _baseCatchModule as RobotCatchModule;
    public RobotSkillModule RobotSkillModule => _baseSkillModule as RobotSkillModule;
    public RobotItemModule RobotItemModule => _baseItemModule as RobotItemModule;
    public RobotTailModule RobotTailModule => _baseTailModule as RobotTailModule;
    public RobotDataSO RobotDataSO => _basePlayerDataSO as RobotDataSO;
    #endregion

    public override void Init(bool isPlayer, int playerId, PlayerState playerState, int tailIndex, int targetTailIndex)
    {
        base.Init(isPlayer, playerId, playerState, tailIndex, targetTailIndex);

        AddComponents();

        InitComponent();
    }

    private void AddComponents()
    {
        _baseInputModule = gameObject.AddComponent<RobotInputModule>();
        _baseMoveModule = gameObject.AddComponent<RobotMoveModule>();
        _baseCatchModule = gameObject.AddComponent<RobotCatchModule>();
        _baseSkillModule = gameObject.AddComponent<RobotSkillModule>();
        _baseItemModule = gameObject.AddComponent<RobotItemModule>();
        _baseTailModule = gameObject.AddComponent<RobotTailModule>();
    }
}
