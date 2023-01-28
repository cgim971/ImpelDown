using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour {
    public void Init(RoomInfo roomInfo) {
        GetComponent<Button>().onClick.AddListener(() => {
            RoomListManager.Instance.JoinRoom(roomInfo.RoomIndex);
        });
    }
}
