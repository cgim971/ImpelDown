using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianTailModule : BaseTailModule
{
    public MagicianPlayer MagicianPlayer => _player as MagicianPlayer;

    public override void Init()
    {
        _player = GetComponent<MagicianPlayer>();

        base.Init();
    }
}
