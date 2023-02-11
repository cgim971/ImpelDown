using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerPanel : MonoBehaviour {
    private Text _playerText;

    private void Awake() {
        _playerText = transform.Find("PlayerText").GetComponent<Text>();
    }

    public void Init(PlayerInfo playerInfo = null) {
        _playerText.text = playerInfo != null ? playerInfo.PlayerId.ToString() : "???";
    }

}
