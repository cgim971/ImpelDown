using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SGameStartHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Game_Start msg = packet as S_Game_Start;

        RoomManager.Instance.StartGame(msg.PlayerAllDatas, msg.MapData.MapIndex);
    }

}
