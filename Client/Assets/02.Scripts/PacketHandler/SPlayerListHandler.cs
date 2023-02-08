using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerListHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Player_List msg = packet as S_Player_List;

        PlayerManager.Instance.UpdateRemotePlayer(msg.PlayerAllData);
    }
}
