using Cinemachine;
using Google.Protobuf;
using Google.Protobuf.Collections;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {

    #region Property
    public static RoomManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<RoomManager>();

            return _instance;
        }
        set => _instance = value;
    }

    public RoomData RoomData { get => _roomData; set => _roomData = value; }

    #endregion
    private static RoomManager _instance = null;

    private RoomData _roomData = null;

    public void Init() { }

    public void StartGame(RepeatedField<PlayerAllData> playerAllDataList, int roomIndex) {
        string mapName = string.Empty;
        switch (roomIndex) {
            case 0:
                mapName = "Forest";
                break;
        }

        SceneManager.LoadScene(mapName);

        StartCoroutine(CreatePlayer(playerAllDataList));
    }

    private IEnumerator CreatePlayer(RepeatedField<PlayerAllData> playerAllDataList) {
        yield return null;

        PlayerManager.Instance = new PlayerManager();

        foreach (PlayerAllData playerAllData in playerAllDataList) {
            PlayerController newPlayer = null;
            switch (playerAllData.PlayerData.PlayerCharacterIndex) {
                case 0:
                    newPlayer = Instantiate(Resources.Load<GameObject>("Prefabs/Players/Pirate/PiratePlayer")).GetComponent<PlayerController>();
                    break;
            }
            newPlayer.transform.position = new Vector2(playerAllData.PosAndRot.X, playerAllData.PosAndRot.Y);

            if (playerAllData.PlayerData.PlayerId == GameManager.Instance.PlayerInfo.PlayerId) {
                newPlayer.Init(true, playerAllData.PlayerData.PlayerId, playerAllData.PlayerData.TailIndex);
                GameObject.Find("FollowCam").GetComponent<CinemachineVirtualCamera>().m_Follow = newPlayer.transform;
            }
            else {
                newPlayer.Init(false, playerAllData.PlayerData.PlayerId, playerAllData.PlayerData.TailIndex);
            }

            PlayerManager.Instance.AddRemotePlayer(newPlayer);
        }
    }
}
