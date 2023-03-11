using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Player msg = packet as S_Player;
        MatchManager.Instance.Init(msg.PlayerInfo);
    }
}
