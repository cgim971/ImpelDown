using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListManager : MonoBehaviour {

    #region Property
    public static RoomListManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<RoomListManager>();

            return _instance;
        }
        set { _instance = value; }
    }
    #endregion
    private static RoomListManager _instance = null;

    private GameObject _roomPanel;
    private Transform _content;

    public void Init(GameObject roomPanel, Transform content, Button createRoomBtn, Button refreshRoomListBtn, Button exitRoomBtn) {
        _roomPanel = roomPanel;
        _content = content;

        createRoomBtn.onClick.AddListener(() => CreateRoom());
        refreshRoomListBtn.onClick.AddListener(() => RefreshRoom());
        exitRoomBtn.onClick.AddListener(() => ExitRoom());
    }

    public void RoomIn() {
        Debug.Log("Room In");
    }

    public void RoomOut() {
        Debug.Log("Room Out");
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
        Transform[] childList = _content.gameObject.GetComponentsInChildren<Transform>();

        if (childList != null) {
            for (int i = 1; i < childList.Length; i++) {
                if (childList[i] != _content) {
                    Destroy(childList[i].gameObject);
                }
            }
        }

        for (int i = 0; i < roomInfoList.Count; i++) {
            GameObject newRoomPanel = Instantiate(_roomPanel, _content);
            newRoomPanel.GetComponent<RoomPanel>().Init(roomInfoList[i]);
        }
    }
}
