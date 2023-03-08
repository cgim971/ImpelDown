using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonTailMoudule : BaseTailModule
{
    public RaccoonPlayer RaccoonPlayer => _player as RaccoonPlayer;

    public override void Init()
    {
        _player = GetComponent<RaccoonPlayer>();

        base.Init();
    }
}
