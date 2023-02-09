using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SExitRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Exit_Room msg = packet as S_Exit_Room;
        MatchManager.Instance.RoomOut();
    }
}
