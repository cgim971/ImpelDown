using Google.Protobuf.Collections;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class RoomInPanelUI : MonoBehaviour {

    #region Property

    public bool IsHost {
        get => _isHost;
        set => _isHost = value;
    }

    #endregion

    [SerializeField] private List<UserUIData> _userUIDataList = new List<UserUIData>();
    [SerializeField] private TMP_Text _roomNameText;


    private bool _isHost = false;


    private void Start() { }

    public void Init() {
        foreach (UserUIData user in _userUIDataList) {
            user.UserUI.Init();
            user.IsLock = false;
        }
    }

    public void RefreshRoomData(RoomInfo roomInfo) {
        _roomNameText.SetText($"{roomInfo.HostName}ÀÇ ¹æ");
        _isHost = roomInfo.HostId == GameManager.Instance.PlayerId ? true : false;

        for (int i = 0; i < 8; i++) {
            _userUIDataList[i].UserUI.Refresh(roomInfo.RoomDatas[i].PlayerName);
        }
    }
}

[System.Serializable]
class UserUIData {
    public UserUI UserUI;
    public bool IsLock;
}