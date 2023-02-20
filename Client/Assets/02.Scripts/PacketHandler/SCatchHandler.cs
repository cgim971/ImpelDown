using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCatchHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Catch msg = packet as S_Catch;

        PlayerManager.Instance.UpdatePlayerTargetTailIndex(msg.CatchingPlayerInfo);
        PlayerManager.Instance.UpdatePlayerState(msg.PlayerInfos);
    }
}

