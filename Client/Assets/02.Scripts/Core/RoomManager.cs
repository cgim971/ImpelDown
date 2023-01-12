using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    // 방을 생성할 때 방 전용 소켓을 생성
    // 거기서 방 관련 이벤트 담당

    public int maximumPeople= 5;
    public int joinRoomIndex= 0;
    public int playerId = 0;
    public int deleteRoomIndex= 3;

    private void Update() {
        // Test code
        if (Input.GetKeyDown(KeyCode.A)) {
            C_Create_Room cCreateRoom = new C_Create_Room { MaximumPeople = maximumPeople };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);   
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            PlayerInfo playerInfo = new PlayerInfo { PlayerId = playerId };
            C_Join_Room cJoinRoom = new C_Join_Room { RoomIndex = joinRoomIndex, PlayerInfo= playerInfo };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CJoinRoom, cJoinRoom);
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            C_Delete_Room cDeleteRoom = new C_Delete_Room { RoomIndex = deleteRoomIndex };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CDeleteRoom, cDeleteRoom);
        }

    }

}
