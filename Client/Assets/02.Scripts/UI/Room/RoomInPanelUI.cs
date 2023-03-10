using Google.Protobuf.Collections;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomInPanelUI : MonoBehaviour {

    #region Property

    public bool IsHost {
        get => _isHost;
        set => _isHost = value;
    }

    #endregion

    [SerializeField] private List<UserUIData> _userUIDataList = new List<UserUIData>();
    [SerializeField] private TMP_Text _roomNameText;

    [SerializeField] private Button _exitBtn;
    [SerializeField] private Button _readyBtn;


    private bool _isHost = false;


    private void Start() {
        _exitBtn.onClick.AddListener(() => ExitRoom());
    }

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

        MatchManager.Instance.RoomMapPanelUI.Init(_isHost, roomInfo.MapIndex);
    }

    public void ExitRoom() {
        C_ExitRoom data = new C_ExitRoom { PlayerId = GameManager.Instance.PlayerId };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CExitRoom, data);
    }
}

[System.Serializable]
class UserUIData {
    public UserUI UserUI;
    public bool IsLock;
}