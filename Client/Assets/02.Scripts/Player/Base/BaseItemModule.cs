using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemModule : MonoBehaviour {
    #region Property
    public BasePlayer Player => _player;
    #endregion

    protected BasePlayer _player;

    public abstract void Init();

    // 아이템 사용 R
}
