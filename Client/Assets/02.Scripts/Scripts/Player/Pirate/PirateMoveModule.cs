using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���⵵ ���� ��ǲ Ŭ������ ����
/// </summary>
public class PirateMoveModule : BaseMoveModule
{
    //�̰� baseplayer�� pirtateplayer�� �ٲ��ִ� �� <- ������ ���� ��� ������ �� ���÷� �־��
    PiratePlayer Player => player as PiratePlayer;

    protected override void Start()
    {
        base.Start();
    }
}
