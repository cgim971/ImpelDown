using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInitHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Init msg = packet as S_Init;

        PlayerController player  = GameManager.Instance.SetPlayer();
        player.PlayerId = msg.PlayerInfo.PlayerId;

        C_Enter cEnter = new C_Enter { PlayerInfo = msg.PlayerInfo };

        NetworkManager.Instance.RegisterSend((ushort)MSGID.CEnter, cEnter);
    }
}
