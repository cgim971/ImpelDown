using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseItemModule : MonoBehaviour {
    #region Property
    public BasePlayer Player => _player;
    #endregion

    protected BasePlayer _player;


    public virtual void Init() {
    }
    

}
