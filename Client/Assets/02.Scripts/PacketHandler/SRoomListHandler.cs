using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRoomListHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_RoomList msg = packet as S_RoomList;

        List<RoomInfo> roomInfoList = new List<RoomInfo>();
        foreach (RoomInfo roomInfo in msg.RoomInfos) {
            roomInfoList.Add(roomInfo);
        }

        RoomManager.Instance.RefreshRoomList(roomInfoList);
    }
}
