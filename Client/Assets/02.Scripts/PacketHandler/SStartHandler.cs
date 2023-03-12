using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SStartHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Start msg = packet as S_Start;
        Debug.Log("start");
    }
}
