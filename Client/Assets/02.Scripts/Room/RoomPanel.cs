using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour {

    public Button Btn;
    private RoomInfo _roomInfo;

    public void Init(RoomInfo roomInfo) {
        _roomInfo = roomInfo;

        Btn.onClick.AddListener(() => {
            C_Join_Room cJoinRoom = new C_Join_Room { PlayerId = GameManager.Instance.PlayerId, RoomId = _roomInfo.RoomId };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CJoinRoom, cJoinRoom);
        });
    }



}
