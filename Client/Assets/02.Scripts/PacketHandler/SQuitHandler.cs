using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQuitHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Quit msg = packet as S_Quit;

        PlayerManager.Instance.RemoveRemotePlayer(msg.PlayerId);
    }
}
