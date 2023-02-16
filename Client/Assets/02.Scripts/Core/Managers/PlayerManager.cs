using Google.Protobuf.Collections;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

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

    private Dictionary<int, BasePlayer> _remotePlayerList;

    public PlayerManager() {
        // 새로운 게임 시작할 시 초기화
        _remotePlayerList = new Dictionary<int, BasePlayer>();
    }

    public void CreatePlayer(RepeatedField<PlayerInfo> playerList) {

        foreach (PlayerInfo playerInfo in playerList) {
            BasePlayer newPlayer = GameObject.Instantiate(CharacterManager.Instance.PlayerCharacterPrefab(playerInfo.CharacterIndex));
            bool isPlayer = (playerInfo.PlayerId == GameManager.Instance.PlayerId) ? true : false;
            newPlayer.Init(isPlayer, playerInfo.PlayerId, playerInfo.TailIndex);
            AddRemotePlayer(newPlayer);
        }
    }

    public void AddRemotePlayer(BasePlayer player) {
        Debug.Log(player.PlayerId);
        _remotePlayerList.Add(player.PlayerId, player);
    }

    public void RemoveRemotePlayer(int playerId) {
        BasePlayer player = null;
        if (_remotePlayerList.TryGetValue(playerId, out player)) {
            _remotePlayerList.Remove(playerId);
            GameObject.Destroy(player.gameObject);
        }
    }

    public void UpdateRemotePlayer(RepeatedField<PlayerInfo> playerInfos) {
        Debug.Log(playerInfos.Count);
        foreach (PlayerInfo playerInfo in playerInfos) {
            PositionData positionData = Util.ChangePosition(playerInfo.PositionInfo);

            BasePlayer player = null;
            if (_remotePlayerList.TryGetValue(playerInfo.PlayerId, out player)) {
                player.SetPositionInfo(positionData);
            }
        }
    }
}