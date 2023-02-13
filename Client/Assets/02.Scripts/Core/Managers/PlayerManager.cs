using Google.Protobuf.Collections;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerManager {
    #region Property
    public static PlayerManager Instance {
        get {
            if (_instance == null)
                _instance = new PlayerManager();

            return _instance;
        }
        set => _instance = value;
    }
    #endregion
    private static PlayerManager _instance = null;

    private Dictionary<int, Player> _remotePlayerList;

    public PlayerManager() {
        // ���ο� ���� ������ �� �ʱ�ȭ
        _remotePlayerList = new Dictionary<int, Player>();
    }

    public void CreatePlayer(RepeatedField<PlayerInfo> playerList) {

        foreach (PlayerInfo playerInfo in playerList) {
            Player newPlayer = GameObject.Instantiate(CharacterManager.Instance.PlayerCharacterPrefab(playerInfo.CharacterIndex));
            bool isPlayer = playerInfo.PlayerId == GameManager.Instance.PlayerId ? true : false;
            newPlayer.Init(isPlayer, playerInfo.PlayerId);
        }
    }
}