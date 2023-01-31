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

    public void Init(GameObject roomPanel, Transform content) {
        _roomPanel = roomPanel;
        _content = content;

        SceneLobbyManager.Instance.CreateRoomBtn.onClick.AddListener(() => CreateRoom());
        SceneLobbyManager.Instance.RefreshRoomListBtn.onClick.AddListener(() => RefreshRoom());
        SceneLobbyManager.Instance.ExitRoomBtn.onClick.AddListener(() => ExitRoom());
    }

    public void RoomIn() {
        SceneLobbyManager.Instance.RoomInPanel.SetActive(true);
        SceneLobbyManager.Instance.RoomOutPanel.SetActive(false);

        RefreshRoomInfo();

        if (RoomManager.Instance.RoomData.HostId == GameManager.Instance.PlayerInfo.PlayerId) {
            SceneLobbyManager.Instance.StartBtn.SetActive(true);
        }
        else {
            SceneLobbyManager.Instance.StartBtn.SetActive(false);
        }
    }

    public void RefreshRoomInfo() {
        RoomData roomInfo = RoomManager.Instance.RoomData;
        SceneLobbyManager.Instance.Text.text = $"Room Index : {roomInfo.RoomIndex}\n {roomInfo.CurrentPeople} / {roomInfo.MaxPeople}";
    }

    public void RoomOut() {
        SceneLobbyManager.Instance.RoomInPanel.SetActive(false);
        SceneLobbyManager.Instance.RoomOutPanel.SetActive(true);
    }


    public void CreateRoom() {
        RoomData roomData = new RoomData { PlayerId = GameManager.Instance.PlayerInfo.PlayerId, MaxPeople = 4 };
        C_Create_Room cCreateRoom = new C_Create_Room { RoomData = roomData };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);
    }

    public void JoinRoom(int index) {
        RoomData roomData = new RoomData { PlayerId = GameManager.Instance.PlayerInfo.PlayerId, RoomIndex = index };
        C_Join_Room cJoinRoom = new C_Join_Room { RoomData = roomData };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CJoinRoom, cJoinRoom);
    }

    public void ExitRoom() {
        RoomData roomData = new RoomData { PlayerId = GameManager.Instance.PlayerInfo.PlayerId };
        C_Exit_Room cExitRoom = new C_Exit_Room { RoomData = roomData };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CExitRoom, cExitRoom);
    }

    public void RefreshRoom() {
        C_Refresh_Room cRefreshRoom = new C_Refresh_Room();
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CRefreshRoom, cRefreshRoom);
    }

    public void RefreshRoomList(List<RoomData> roomInfoList) {
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
            if (RoomManager.Instance.RoomData != null) {
                if (roomInfoList[i].RoomIndex == RoomManager.Instance.RoomData.RoomIndex) {
                    RoomManager.Instance.RoomData = roomInfoList[i];
                    RefreshRoomInfo();
                }
            }
            newRoomPanel.GetComponent<RoomPanel>().Init(roomInfoList[i]);
        }
    }
}
