using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRefreshRoomListHandler : IPacketHandler {
    public void Process(IMessage packet) {
        SRefreshRoomListHandler msg = packet as SRefreshRoomListHandler;

        Debug.Log(msg); 
    }
}
