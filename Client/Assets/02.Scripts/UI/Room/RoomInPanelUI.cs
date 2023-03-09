using Google.Protobuf.Collections;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoomInPanelUI : MonoBehaviour {


    [SerializeField] private List<UserUIData> _userUIDataList = new List<UserUIData>();

    private void Start() { }

    public void Init() {
        foreach (UserUIData user in _userUIDataList) {
            user.UserUI.Init();
            user.IsLock = false;
        }
    }

    public void RefreshRoomData(RoomInfo roomInfo) {
        Debug.Log(roomInfo.CurrentPeople);
        for (int i = 0; i < 8; i++) {
            //roomInfo.RoomDatas[i].PlayerId
            _userUIDataList[i].UserUI.Refresh("");
        }
    }


}

[System.Serializable]
class UserUIData {
    public UserUI UserUI;
    public bool IsLock;
}