using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCatchingHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Catching msg = packet as S_Catching;

        PlayerManager.Instance.UpdatePlayerTargetTailIndex(msg.PlayerInfo);
    }
}