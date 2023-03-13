using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public abstract class BaseCatchModule : MonoBehaviour {
    #region Property
    public BasePlayer Player => _player;

    public bool IsCatchable { get => _isCatchable; set => _isCatchable = value; }
    #endregion

    protected BasePlayer _player;

    protected LayerMask _playerLayerMask;

    private bool _isCatchable = true;

    public virtual void Init() {
        _playerLayerMask = LayerMask.GetMask("Player");
    }

    public virtual void Catch() {
        if (_isCatchable == false || Input.GetKey(KeyCode.Space) == false)
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        if (_player.PlayerState == PlayerState.Ghost)
            return;

        RaycastHit2D[] players = Physics2D.CircleCastAll(transform.position, 1.5f, Vector2.right);
        foreach (RaycastHit2D player in players) {
            int beCatchedPlayerId = player.transform.GetComponent<BasePlayer>().PlayerId;

            if (beCatchedPlayerId == Player.PlayerId)
                continue;

            //C_Catch cCatch = new C_Catch {
            //    //PlayerId = Player.PlayerId,
            //    //BeCatchedPlayerId = beCatchedPlayerId,
            //};

            //NetworkManager.Instance.RegisterSend((ushort)MSGID.CCatch, cCatch);
            return;
        }
        StartCoroutine(CatchTime());
    }

    private IEnumerator CatchTime() {
        yield return null;
        _isCatchable = false;
    }
}
