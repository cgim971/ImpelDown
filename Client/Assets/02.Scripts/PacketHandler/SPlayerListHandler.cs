using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerListHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_PlayerList msg = packet as S_PlayerList;
        PlayerManager.Instance.UpdateRemotePlayer(msg.PlayerInfos);
    }
}