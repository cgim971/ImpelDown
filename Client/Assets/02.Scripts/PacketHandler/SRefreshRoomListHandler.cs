using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRefreshRoomListHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Refresh_Room_List msg = packet as S_Refresh_Room_List;

        List<RoomInfo> roomInfoList = new List<RoomInfo>();
        foreach (RoomInfo roomInfo in msg.RoomInfos) {
            Debug.Log(roomInfo);
            roomInfoList.Add(roomInfo);
        }

        RoomManager.Instance.RefreshRoomList(roomInfoList);
    }
}
