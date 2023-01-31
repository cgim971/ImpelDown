using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SExitRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {

        RoomManager.Instance.RoomData = null;

        RoomListManager.Instance.RoomOut();
    }
}
