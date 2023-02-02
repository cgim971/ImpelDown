using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCatch : MonoBehaviour {

    private PlayerController _playerController;

    private LayerMask _playerLayerMask;
    private Camera _mainCam = null;

    private int _playerId = 0;
    private int _targetId = 0;

    public void Init(PlayerController playerController) {
        _playerController = playerController;
        _playerId = playerController.PlayerId;

        _playerLayerMask = LayerMask.GetMask("Player");
        _mainCam = Camera.main;
    }


    public void CheckInput() {
        Catch();
    }

    private void Catch() {
        if (!Input.GetMouseButtonDown(0)) {
            return;
        }

        Vector3 worldMousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0;

        Vector3 delta = worldMousePos - transform.position;
        delta.Normalize();


        RaycastHit2D[] players = Physics2D.RaycastAll(transform.position, delta, 1.5f, _playerLayerMask);
        foreach (RaycastHit2D player in players) {
            int playerId = player.transform.GetComponent<PlayerController>().PlayerId;

            if (playerId == _playerId)
                continue;

            Debug.Log("PlayerId : " + _playerId);
            Debug.Log("TargetId : " + _targetId);

            if (playerId == _targetId) {
                C_Catch cCatch = new C_Catch {
                    PlayerData = new PlayerData {
                        PlayerId = _targetId,
                        RoomIndex = RoomManager.Instance.RoomData.RoomIndex,
                    }
                };

                NetworkManager.Instance.RegisterSend((ushort)MSGID.CCatch, cCatch);
                Debug.Log("KILL");
            }
            return;
        }
    }

    public void SetTargetId(int targetId) {
        _targetId = targetId;
    }
}
