using Google.Protobuf.Collections;
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

    [SerializeField] private RoomPanel _roomPanel;
    [SerializeField] private RectTransform _content;

    [SerializeField] private CanvasGroup _roomInCanvasGroup;
    [SerializeField] private CanvasGroup _roomOutCanvasGroup;

    private void Awake() {
        RoomOut();
    }

    public void CreateRoom(int maxPeople) {
        maxPeople = Mathf.Clamp(maxPeople, 2, 8);
        C_Create_Room cCreateRoom = new C_Create_Room { PlayerId = GameManager.Instance.PlayerId, MaxPeople = maxPeople };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, cCreateRoom);
    }

    public void JoinRoom(int roomIndex) {
        C_Join_Room cJoinRoom = new C_Join_Room { PlayerId = GameManager.Instance.PlayerId, RoomIndex = roomIndex };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CJoinRoom, cJoinRoom);
    }

    public void ExitRoom() {
        C_Exit_Room cExitRoom = new C_Exit_Room { PlayerId = GameManager.Instance.PlayerId };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CExitRoom, cExitRoom);
    }

    public void MatchMaking() {
        C_Match_Making cMatchMaking = new C_Match_Making { PlayerId = GameManager.Instance.PlayerId };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CMatchMaking, cMatchMaking);
    }

    public void RefreshRoomList() {
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CRefreshRoomlist, new C_Refresh_RoomList());
    }

    public void RefreshRoomList(RepeatedField<RoomInfo> roomInfos) {

        Transform[] childList = _content.GetComponentsInChildren<Transform>();

        if (childList != null) {
            for (int i = 1; i < childList.Length; i++) {
                if (childList[i] != _content) {
                    Destroy(childList[i].gameObject);
                }
            }
        }

        foreach (RoomInfo roomInfo in roomInfos) {
            RoomPanel newRoomPanel = Instantiate(_roomPanel, _content);
            newRoomPanel.Init(roomInfo);
        }
    }

    public void RoomIn() {
        SetCanvasGroup(_roomInCanvasGroup, 1, true, true);
        SetCanvasGroup(_roomOutCanvasGroup);
    }

    public void RoomOut() {
        SetCanvasGroup(_roomOutCanvasGroup, 1, true, true);
        SetCanvasGroup(_roomInCanvasGroup);
    }

    public void SetCanvasGroup(CanvasGroup canvasGroup, float alpha = 0f, bool interactable = false, bool blocksRaycasts = false) {
        canvasGroup.alpha = alpha;
        canvasGroup.interactable = interactable;
        canvasGroup.blocksRaycasts = blocksRaycasts;
    }
}
