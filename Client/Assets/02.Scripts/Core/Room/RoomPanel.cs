using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour {
    public void Init(RoomData roomInfo) {
        GetComponentInChildren<Text>().text = $"Room Index : {roomInfo.RoomIndex}\n {roomInfo.CurrentPeople} / {roomInfo.MaxPeople}";
        GetComponent<Button>().onClick.AddListener(() => {
            RoomListManager.Instance.JoinRoom(roomInfo.RoomIndex);
        });
    }
}
