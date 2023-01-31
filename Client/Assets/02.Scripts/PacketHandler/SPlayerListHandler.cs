using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerListHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Player_List msg = packet as S_Player_List;

        Debug.Log("@");
        foreach (PlayerAllData playerAllData in msg.PlayerAllData) {
            Debug.Log($"Player Id : {playerAllData.PlayerData.PlayerId}");
            Debug.Log($"x : {playerAllData.PosAndRot.X}, y : {playerAllData.PosAndRot.Y}");
            Debug.Log($"rot : {playerAllData.PosAndRot.Rot}");
        }
    }
}
