using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCreateRoomHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Create_Room msg = packet as S_Create_Room;

        //foreach (RoomInfo room in msg.RoomInfos) {
        //    Debug.Log($"Room : {room.RoomId}");
        //    Debug.Log($"{room.CurrentPeople} / {room.MaximumPeople}");
        //    foreach (PlayerInfo player in room.PlayerInfos) {
        //        Debug.Log(player.PlayerId);
        //    }
        //}
    }
}
