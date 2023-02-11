using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRefreshRoomListHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Refresh_RoomList msg = packet as S_Refresh_RoomList;
        MatchManager.Instance.RefreshRoomList(msg.RoomInfos);
    }
}