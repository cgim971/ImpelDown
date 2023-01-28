using Google.Protobuf;
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

    public GameObject RoomPanel;
    public Transform Content;

    public void Init(GameObject roomPanel, Transform content) {
        RoomPanel = roomPanel;
        Content = content;
    }

    public void CreateRoom() {
        RoomInfo roomInfo = new RoomInfo { PlayerId = GameManager.Instance.PlayerController.PlayerId, MaxPeople = 4 };
        C_Create_Room cCreateRoom = new C_Create_Room { RoomInfo = roomInfo };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);
    }

    public void JoinRoom(int index) {
        RoomInfo roomInfo = new RoomInfo { PlayerId = GameManager.Instance.PlayerController.PlayerId, RoomIndex = index };
        C_Join_Room cJoinRoom = new C_Join_Room { RoomInfo = roomInfo };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CJoinRoom, cJoinRoom);
    }

    public void ExitRoom() {
        RoomInfo roomInfo = new RoomInfo { PlayerId = GameManager.Instance.PlayerController.PlayerId };
        C_Exit_Room cExitRoom = new C_Exit_Room { RoomInfo = roomInfo };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CExitRoom, cExitRoom);
    }

    public void RefreshRoom() {
        C_Refresh_Room cRefreshRoom = new C_Refresh_Room();
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CRefreshRoom, cRefreshRoom);
    }

    public void RefreshRoomList(List<RoomInfo> roomInfoList) {

        Transform[] childList = Content.gameObject.GetComponentsInChildren<Transform>();

        if (childList != null) {
            for (int i = 1; i < childList.Length; i++) {
                if (childList[i] != Content) {
                    Destroy(childList[i].gameObject);
                }
            }
        }

        for (int i = 0; i < roomInfoList.Count; i++) {
            GameObject newRoomPanel = Instantiate(RoomPanel, Content);
            newRoomPanel.GetComponent<RoomPanel>().Init(roomInfoList[i]);
        }
    }
}
