using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCatchedHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Catched msg = packet as S_Catched;

        PlayerManager.Instance.RemoveRemotePlayer(msg.PlayerId);
    }
}  