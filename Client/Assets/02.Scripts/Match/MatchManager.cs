using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {

    #region Property
    public static MatchManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<MatchManager>();

            return _instance;
        }
    }
    #endregion
    private static MatchManager _instance = null;

    public void CreateRoom(int maxPeople) {
        maxPeople = Mathf.Clamp(maxPeople, 2, 8);
        C_Create_Room cCreateRoom = new C_Create_Room { PlayerId = GameManager.Instance.PlayerId, MaxPeople = maxPeople };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);
    }

    public void MatchMaking() {
        C_Match_Making cMatchMaking = new C_Match_Making { PlayerId = GameManager.Instance.PlayerId };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CMatchMaking, cMatchMaking);
    }

    public void RefreshRoomList() {
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CRefreshRoomlist, new C_Refresh_RoomList());
    }
}
