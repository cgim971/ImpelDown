using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomOutPanelUI : MonoBehaviour {
    [SerializeField] private Button _createRoomBtn;
    [SerializeField] private Button _matchBtn;

    private void Start() {
        _createRoomBtn.onClick.AddListener(() => CreateRoom());
        _matchBtn.onClick.AddListener(() => MatchMaking());
    }

    public void CreateRoom() {
        C_CreateRoom data =new C_CreateRoom { PlayerId = GameManager.Instance.PlayerId };   
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CCreateRoom, data);
    }

    public void MatchMaking() {
        C_MatchMaking data = new C_MatchMaking { PlayerId = GameManager.Instance.PlayerId };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CMatchMaking, data);
    }
}
