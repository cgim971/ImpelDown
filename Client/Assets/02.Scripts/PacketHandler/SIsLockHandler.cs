using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIsLockHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_IsLock msg = packet as S_IsLock;
        MatchManager.Instance.RoomInPanelUI.RefreshRoomData(msg.RoomInfo);
    }
}
