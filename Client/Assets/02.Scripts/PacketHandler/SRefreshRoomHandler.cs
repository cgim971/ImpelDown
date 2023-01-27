using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRefreshRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Refresh_Room msg = packet as S_Refresh_Room;

        Debug.Log("Room Info : "+msg.RoomInfo.ToString());
    }
}
