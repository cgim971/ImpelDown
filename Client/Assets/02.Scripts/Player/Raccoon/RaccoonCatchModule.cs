using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonCatchModule : BaseCatchModule
{
    public RaccoonPlayer RacccoonPlayer => _player as RaccoonPlayer;

    public override void Init()
    {
        _player = GetComponent<RaccoonPlayer>();
        base.Init();
    }
}
