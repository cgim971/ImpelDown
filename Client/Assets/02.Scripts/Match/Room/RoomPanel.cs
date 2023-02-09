using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour {

    private Text _hostPlayerText;
    private Text _playerCountText;
    private Button _joinBtn;

    private void Awake() {
        _hostPlayerText = transform.Find("HostPlayerText").GetComponent<Text>();
        _playerCountText = transform.Find("PlayerCountText").GetComponent<Text>();
        _joinBtn = transform.Find("JoinBtn").GetComponent<Button>();
    }

    public void Init(RoomInfo roomInfo) {
        _hostPlayerText.text = roomInfo.HostPlayer.PlayerId.ToString();
        _playerCountText.text = $"{roomInfo.CurrentPeople} / {roomInfo.MaxPeople}";
        _joinBtn.onClick.AddListener(() => {
            MatchManager.Instance.JoinRoom(roomInfo.RoomIndex);
        });
    }

}
