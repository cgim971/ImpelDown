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

    public BasePlayer RemotePlayer {
        set => _remotePlayer = value;
    }
    #endregion
    private static PlayerManager _instance = null;
    private Dictionary<int, BasePlayer> _remotePlayerList;
    private BasePlayer _remotePlayer;

    public PlayerManager() {
        // 새로운 게임 시작할 시 초기화
        _remotePlayerList = new Dictionary<int, BasePlayer>();
    }

    public void CreatePlayer(RepeatedField<PlayerInitData> playerDataList) {
        foreach (PlayerInitData playerData in playerDataList) {
            BasePlayer newPlayer = GameObject.Instantiate(CharacterManager.Instance.GetPlayer(playerData.CharacterIndex));
            bool isPlayer = (playerData.PlayerId == GameManager.Instance.PlayerId) ? true : false;
            newPlayer.Init(isPlayer, playerData.PlayerId, playerData.PlayerState, playerData.TailIndex);
            PositionData positionData = Util.ChangePosition(playerData.PlayerPosData);
            newPlayer.SetPositionInfo(positionData, true);

            AddRemotePlayer(newPlayer);
        }
    }

    public void AddRemotePlayer(BasePlayer player) {
        _remotePlayerList.Add(player.PlayerId, player);
    }

    public void RemoveRemotePlayer(int playerId) {
        BasePlayer player = null;
        if (_remotePlayerList.TryGetValue(playerId, out player)) {
            _remotePlayerList.Remove(playerId);
            GameObject.Destroy(player.gameObject);
        }
    }

    public void UpdateRemotePlayer(RepeatedField<PlayerInGameData> playerDataList) {
        foreach (PlayerInGameData playerData in playerDataList) {
            PositionData positionData = Util.ChangePosition(playerData.PlayerPosData);

            BasePlayer player = null;
            if (_remotePlayerList.TryGetValue(playerData.PlayerId, out player)) {
                player.SetPositionInfo(positionData);
            }
        }
    }


    public void UpdatePlayerState(RepeatedField<PlayerInGameData> playerDataList) {
        foreach (PlayerInGameData playerData in playerDataList) {
            BasePlayer player = null;
            if (_remotePlayerList.TryGetValue(playerData.PlayerId, out player)) {
                player.SetPlayerState(playerData.PlayerState);
            }
        }
    }
}