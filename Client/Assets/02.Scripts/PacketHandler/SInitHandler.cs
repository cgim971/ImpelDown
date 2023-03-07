using Google.Protobuf;
using ImpelDown.Proto;
using UnityEngine;

public class SInitHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Init msg = packet as S_Init;

        Debug.Log(msg.PlayerInfo.PlayerId);
    }
}
