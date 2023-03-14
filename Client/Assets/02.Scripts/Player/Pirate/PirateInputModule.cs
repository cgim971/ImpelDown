using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PirateInputModule : BaseInputModule {
    public PiratePlayer PiratePlayer => _player as PiratePlayer;

    public override void Init() {
        _player = GetComponent<PiratePlayer>();
    }
}
