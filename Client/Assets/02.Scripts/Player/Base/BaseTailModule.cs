using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTailModule : MonoBehaviour {

    #region Property
    public BasePlayer Player => _player;
    #endregion

    protected BasePlayer _player;

    public abstract void Init();

}
