using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    #region Property
    public static RoomManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<RoomManager>();

            return _instance;
        }
        set { _instance = value; }
    }
    #endregion

    private static RoomManager _instance = null;

    public void Init() { }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            RoomInfo roomInfo = new RoomInfo { PlayerId = GameManager.Instance.PlayerController.PlayerId, MaxPeople = 4 };
            C_Create_Room cCreateRoom = new C_Create_Room { RoomInfo = roomInfo };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);
        }
    }
}
