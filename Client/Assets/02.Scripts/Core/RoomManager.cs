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
    }
    #endregion
    private static RoomManager _instance = null;

    public GameObject Room;
    public Transform Content;


    public void CreateRoom() {
        PlayerInfo hostInfo = new PlayerInfo { PlayerId = GameManager.Instance.PlayerId };
        RoomInfo roomInfo = new RoomInfo { HostInfo = hostInfo, MaximumPeople = 4 };
        C_Create_Room cCreateRoom = new C_Create_Room { RoomInfo = roomInfo };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);
    }

    public void RefreshRoom() {
        C_RoomList cRoomList = new C_RoomList();
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CRoomlist, cRoomList);
    }

    public void ExitRoom() {
        PlayerInfo playerInfo = new PlayerInfo { PlayerId = GameManager.Instance.PlayerId };
        C_Exit_Room cExitRoom = new C_Exit_Room { PlayerInfo = playerInfo };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CExitRoom, cExitRoom);
    }

    public void RefreshRoomList(List<RoomInfo> roomInfos) {
        Transform[] childList = Content.GetComponentsInChildren<Transform>();
        if (childList != null) {
            for (int i = 1; i < childList.Length; i++) {
                if (childList[i] != transform) {
                    Destroy(childList[i].gameObject);
                }
            }
        }

        foreach (RoomInfo roomInfo in roomInfos) {
            GameObject roomPanel = Instantiate(Room, Content);
            roomPanel.GetComponent<RoomPanel>().Init(roomInfo);
        }
    }
}
