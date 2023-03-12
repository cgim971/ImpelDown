using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIsReadyHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_IsReady msg = packet as S_IsReady;
        MatchManager.Instance.RoomInPanelUI.RefreshRoomData(msg.RoomInfo);
    }
}
