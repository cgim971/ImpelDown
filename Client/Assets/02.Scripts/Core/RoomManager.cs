using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            PlayerInfo playerInfo = new PlayerInfo { PlayerId = GameManager.Instance.PlayerId };
            C_Create_Room cCreateRoom = new C_Create_Room { PlayerInfo = playerInfo, MaximumPeople = 4 };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);

            Debug.Log("¹æ »ý¼º");
        }
    }
}
