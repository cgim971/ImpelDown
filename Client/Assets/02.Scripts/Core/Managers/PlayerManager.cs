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

    public void CreatePlayer(RepeatedField<PlayerInGameData> playerList) {
        foreach (PlayerInGameData player in playerList) {
            //BasePlayer newPlayer = GameObject.Instantiate(CharacterManager.Instance.PlayerCharacterPrefab(player.CharacterIndex));
            //bool isPlayer = (player.PlayerId == GameManager.Instance.PlayerId) ? true : false;
            ////newPlayer.Init(isPlayer, player.PlayerId, player.PlayerState, player.TailIndex, player.);
            //PositionData positionData = Util.ChangePosition(player.PlayerPosData);
            //newPlayer.SetPositionInfo(positionData, true);

            //AddRemotePlayer(newPlayer);
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

    public void UpdateRemotePlayer(RepeatedField<PlayerInGameData> playerInfos) {
        foreach (PlayerInGameData player in playerInfos) {
            PositionData positionData = Util.ChangePosition(player.PlayerPosData);

            //BasePlayer player = null;
            //if (_remotePlayerList.TryGetValue(player.PlayerId, out player)) {
            //    player.SetPositionInfo(positionData);
            //}
        }
    }

    public void UpdatePlayerTargetTailIndex(PlayerInfo playerInfo) {
        if (_remotePlayerList.TryGetValue(playerInfo.PlayerId, out BasePlayer player)) {
            //player.SetTargetTailIndex(playerInfo.TargetTailIndex);
        }
    }

    public void UpdatePlayerState(RepeatedField<PlayerInfo> playerInfos) {
        foreach (PlayerInfo playerInfo in playerInfos) {
            BasePlayer player = null;
            if (_remotePlayerList.TryGetValue(playerInfo.PlayerId, out player)) {
                //player.SetPlayerState(playerInfo.PlayerState);
            }
        }
    }
}