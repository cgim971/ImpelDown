using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SJoinRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Join_Room msg = packet as S_Join_Room;

        RoomListManager.Instance.RoomIn();
    }
}
