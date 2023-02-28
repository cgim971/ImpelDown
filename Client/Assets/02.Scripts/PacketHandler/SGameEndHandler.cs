using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SGameEndHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Game_End msg = packet as S_Game_End;
        Debug.Log("game winner : " + msg.PlayerId.ToString());

        SceneManager.LoadScene("Lobby");
    }
}