using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRefreshRoomListHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Refresh_Room_List msg = packet as S_Refresh_Room_List;

        List<RoomData> roomInfoList = new List<RoomData>();
        foreach (RoomData roomInfo in msg.RoomInfos) {
            roomInfoList.Add(roomInfo);
        }

        RoomListManager.Instance.RefreshRoomList(roomInfoList);
    }
}
