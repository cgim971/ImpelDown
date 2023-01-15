using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCreateRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Create_Room msg = packet as S_Create_Room;


    }
}
