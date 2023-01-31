using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SGameStartHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Game_Start msg = packet as S_Game_Start;

        List<PlayerAllData> playerAllDataList = new List<PlayerAllData>();

        foreach (PlayerAllData playerAllData in msg.PlayerAllDatas) {
            playerAllDataList.Add(playerAllData);
        }

        RoomManager.Instance.StartGame(playerAllDataList, msg.MapData.MapIndex);
    }

}
