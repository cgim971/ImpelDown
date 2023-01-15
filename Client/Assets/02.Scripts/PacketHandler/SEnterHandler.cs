using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEnterHandler : IPacketHandler
{
    public void Process(IMessage packet) {
        S_Enter msg = packet as S_Enter;

        Debug.Log(msg.PlayerInfo.PlayerId);

        GameManager.Instance.PlayerId = msg.PlayerInfo.PlayerId;
    }
}
