using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonInputModule : BaseInputModule
{
    public RaccoonPlayer RaccooonPlayer => _player as RaccoonPlayer;

    public override void Init()
    {
        _player = GetComponent<RaccoonPlayer>();
    }
}
