using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTailModule : MonoBehaviour {

    #region Property
    public BasePlayer Player => _player;
    #endregion

    protected BasePlayer _player;
    Transform _tailsTs;

    public virtual void Init() {
        _tailsTs = transform.Find("Tails");
    }

    public void SetTail(int tailIndex) {
        _player.TailIndex = tailIndex;

        CreateTail();
    }

    public void CreateTail() {
        GameObject tail = Instantiate(TailManager.Instance.GetTail(_player.TailIndex), transform);

    }

}

