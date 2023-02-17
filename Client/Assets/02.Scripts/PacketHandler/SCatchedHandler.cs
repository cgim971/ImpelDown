using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCatchedHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Catched msg = packet as S_Catched;

        // 30초 시간 필요
        // PlayerManager.Instance.RemoveRemotePlayer(msg.PlayerId);
    }
}