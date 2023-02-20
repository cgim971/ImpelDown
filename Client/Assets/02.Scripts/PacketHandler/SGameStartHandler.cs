using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameStartHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Game_Start msg = packet as S_Game_Start;

        GameManager.Instance.GameStart(msg.RoomInfo);
    }
}

