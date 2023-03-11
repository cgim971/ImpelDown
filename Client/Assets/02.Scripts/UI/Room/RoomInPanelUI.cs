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

    [SerializeField] private List<UserUI> _userUIList = new List<UserUI>();
    [SerializeField] private TMP_Text _roomNameText;

    [SerializeField] private Button _exitBtn;
    [SerializeField] private Button _readyBtn;

    [SerializeField] private List<Sprite> _playerSpriteList;


    private bool _isHost = false;


    private void Start() {
        _exitBtn.onClick.AddListener(() => ExitRoom());
    }

    public void Init() {
        for (int index = 0; index < _userUIList.Count; index++) {
            _userUIList[index].Init();
        }
    }

    public void RefreshRoomData(RoomInfo roomInfo) {
        _roomNameText.SetText($"{roomInfo.HostName}�� ��");
        _isHost = roomInfo.HostId == GameManager.Instance.PlayerId ? true : false;

        for (int i = 0; i < 8; i++) {
            _userUIList[i].Refresh(GetPlayerSprite(roomInfo.RoomDatas[i]), roomInfo.RoomDatas[i].PlayerId != -1, roomInfo.RoomDatas[i].PlayerName);
        }

        MatchManager.Instance.RoomMapPanelUI.Init(_isHost, roomInfo.MapIndex);
    }

    public void ExitRoom() {
        C_ExitRoom data = new C_ExitRoom { PlayerId = GameManager.Instance.PlayerId };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CExitRoom, data);
    }

    public Sprite GetPlayerSprite(RoomData roomData) {
        if (roomData.IsLock == true) {
            return _playerSpriteList[2];
        }
        else {
            // �÷��̾ �ִ�.
            if (roomData.PlayerId != -1) {
                if (roomData.IsReady == true)
                    return _playerSpriteList[0];

                return _playerSpriteList[1];
            }
        }

        return _playerSpriteList[3];
    }
}