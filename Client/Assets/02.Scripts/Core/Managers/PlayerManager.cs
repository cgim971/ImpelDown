using Google.Protobuf.Collections;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerManager
{
    #region Property
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerManager();

            return _instance;
        }
        set => _instance = value;
    }

    public BasePlayer RemotePlayer
    {
        set => _remotePlayer = value;
    }
    #endregion
    private static PlayerManager _instance = null;
    private Dictionary<int, BasePlayer> _remotePlayerList;
    private BasePlayer _remotePlayer;

    public PlayerManager()
    {
        // 새로운 게임 시작할 시 초기화
        _remotePlayerList = new Dictionary<int, BasePlayer>();
    }

    public void CreatePlayer(RepeatedField<PlayerInfo> playerList)
    {
        foreach (PlayerInfo playerInfo in playerList)
        {
            BasePlayer newPlayer = GameObject.Instantiate(CharacterManager.Instance.PlayerCharacterPrefab(playerInfo.CharacterIndex));
            bool isPlayer = (playerInfo.PlayerId == GameManager.Instance.PlayerId) ? true : false;
            newPlayer.Init(isPlayer, playerInfo.PlayerId, playerInfo.PlayerState, playerInfo.TailIndex, playerInfo.TargetTailIndex);
            PositionData positionData = Util.ChangePosition(playerInfo.PositionInfo);
            newPlayer.SetPositionInfo(positionData, true);

            AddRemotePlayer(newPlayer);
        }
    }

    public Dictionary<ETailName, int> GetPlayerTails(int targetTailIndex)
    {
        List<BasePlayer> list = new List<BasePlayer>();
        Dictionary<ETailName, int> tails = new Dictionary<ETailName, int>();

        foreach (BasePlayer player in _remotePlayerList.Values)
        {
            if(player.TailIndex == targetTailIndex)
            {
                list.Add(player);
                break;
            }
        }

        TailInfo[] tailInfo = list[0].transform.Find("Tails").GetComponentsInChildren<TailInfo>();

        foreach(TailInfo tail in tailInfo)
        {
            tails.Add(tail.name, tail.index);
        }

        return tails;
    }

    public void AddRemotePlayer(BasePlayer player)
    {
        _remotePlayerList.Add(player.PlayerId, player);
    }

    public void RemoveRemotePlayer(int playerId)
    {
        BasePlayer player = null;
        if (_remotePlayerList.TryGetValue(playerId, out player))
        {
            _remotePlayerList.Remove(playerId);
            GameObject.Destroy(player.gameObject);
        }
    }

    public void UpdateRemotePlayer(RepeatedField<PlayerInfo> playerInfos)
    {
        foreach (PlayerInfo playerInfo in playerInfos)
        {
            PositionData positionData = Util.ChangePosition(playerInfo.PositionInfo);

            BasePlayer player = null;
            if (_remotePlayerList.TryGetValue(playerInfo.PlayerId, out player))
            {
                player.SetPositionInfo(positionData);
            }
        }
    }

    public void UpdatePlayerTargetTailIndex(PlayerInfo playerInfo)
    {
        if (_remotePlayerList.TryGetValue(playerInfo.PlayerId, out BasePlayer player))
        {
            player.SetTargetTailIndex(playerInfo.TargetTailIndex);
        }
    }

    public void UpdatePlayerState(RepeatedField<PlayerInfo> playerInfos)
    {
        foreach (PlayerInfo playerInfo in playerInfos)
        {
            BasePlayer player = null;
            if (_remotePlayerList.TryGetValue(playerInfo.PlayerId, out player))
            {
                player.SetPlayerState(playerInfo.PlayerState);
            }
        }
    }
}