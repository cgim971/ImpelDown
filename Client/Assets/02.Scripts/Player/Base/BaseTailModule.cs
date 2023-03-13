using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTailModule : MonoBehaviour {

    #region Property
    public BasePlayer Player => _player;
    #endregion

    protected BasePlayer _player;

    public virtual void Init() { }

    public void SetTail(int tailIndex) {
        _player.TailIndex = tailIndex;
    }

}

