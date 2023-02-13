using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ������ ���� �÷��̾� Ŭ����
/// </summary>
public class PiratePlayer : Player
{
    //��� ����� ���� ���� �ٲ��ִ� �۾� �� �۾��� ���ϸ� ������ ���� ���� �������� �� ���� base��⸸ Ȱ����
    public PirateInputModule InputModule => _inputModule as PirateInputModule;
    public PirateSkillModule SkillModule => _skillModule as PirateSkillModule;
    public PirateMoveModule MoveModule => _moveModule as PirateMoveModule;
    public PirateDataSO PlayerDataSO => _PlayerDataSO as PirateDataSO;

    protected override void Awake()
    {
        base.Awake();
    }
}
