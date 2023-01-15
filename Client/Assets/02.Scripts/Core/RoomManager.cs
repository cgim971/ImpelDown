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

    private Dictionary<int, RoomInfo> _roomDictionary = new Dictionary<int, RoomInfo>();

    public GameObject Room;
    public Transform Content;

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            PlayerInfo playerInfo = new PlayerInfo { PlayerId = GameManager.Instance.PlayerId };
            RoomInfo roomInfo = new RoomInfo { PlayerInfo= playerInfo, MaximumPeople= 4 };
            C_Create_Room cCreateRoom = new C_Create_Room { RoomInfo = roomInfo};
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);
            Debug.Log("¹æ »ý¼º");
        }
    }

    public void RefreshRoom() {
        while (Content.childCount > 0) {
            Destroy(Content.GetChild(0));
        }

        foreach (RoomInfo roomInfo in _roomDictionary.Values) {
            GameObject roomPanel = Instantiate(Room, Content);
            roomPanel.GetComponent<RoomPanel>().RoomInfo = roomInfo;
        }
    }
}
