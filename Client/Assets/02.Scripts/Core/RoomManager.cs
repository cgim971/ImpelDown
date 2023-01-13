using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    // 방을 생성할 때 방 전용 소켓을 생성
    // 거기서 방 관련 이벤트 담당

    public List<PlayerController> _controller = new List<PlayerController>();

    public int maximumPeople = 5;
    public int joinRoomIndex = 0;

    private void Update() {
        // Test code
        if (Input.GetKeyDown(KeyCode.A)) {
            PlayerInfo playerInfo = new PlayerInfo { PlayerId = GameManager.Instance.PlayerController.PlayerId };
            C_Create_Room cCreateRoom = new C_Create_Room { MaximumPeople = maximumPeople, PlayerInfo = playerInfo };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            PlayerInfo playerInfo = new PlayerInfo { PlayerId = GameManager.Instance.PlayerController.PlayerId };
            C_Join_Room cJoinRoom = new C_Join_Room { RoomIndex = joinRoomIndex, PlayerInfo = playerInfo };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CJoinRoom, cJoinRoom);
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            PlayerInfo playerInfo = new PlayerInfo { PlayerId = GameManager.Instance.PlayerController.PlayerId };
            C_Exit_Room cExitRoom = new C_Exit_Room { PlayerInfo = playerInfo };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CExitRoom, cExitRoom);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            PlayerInfo playerInfo = new PlayerInfo { PlayerId = GameManager.Instance.PlayerController.PlayerId };
            C_Delete_Room cDeleteRoom = new C_Delete_Room { PlayerInfo = playerInfo };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CDeleteRoom, cDeleteRoom);
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            C_Debug_Room cDebugRoom = new C_Debug_Room { RoomIndex = 0 };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CDebugRoom, cDebugRoom);
        }
    }

}
