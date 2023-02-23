//using Google.Protobuf;
//using ImpelDown.Proto;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class SGameExitHandler : IPacketHandler {
//    public void Process(IMessage packet) {
//        S_Game_Exit msg = packet as S_Game_Exit;

//        if (GameManager.Instance.PlayerId == msg.PlayerId) {
//            SceneManager.LoadScene("Lobby");
//        }
//        else {
//            PlayerManager.Instance.RemoveRemotePlayer(msg.PlayerId);
//            PlayerManager.Instance.UpdatePlayerTargetTailIndex(msg.PlayerInfo);
//        }
//    }
//}