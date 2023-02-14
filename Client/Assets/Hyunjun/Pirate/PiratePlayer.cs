using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 구현한 해적 플레이어 클래스
/// </summary>
public class PiratePlayer : Player
{
    //모든 모듈을 해적 모듈로 바꿔주는 작업 이 작업을 안하면 해적만 쓰는 각종 변수들을 못 쓰고 base모듈만 활용함
    public PirateInputModule InputModule => _inputModule as PirateInputModule;
    public PirateSkillModule SkillModule => _skillModule as PirateSkillModule;
    public PirateMoveModule MoveModule => _moveModule as PirateMoveModule;
    public PirateDataSO PlayerDataSO => base.PlayerDataSO as PirateDataSO;

    protected override void Awake()
    {
        base.Awake();
    }
}
