using Google.Protobuf;
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

    public void StartGame(List<PlayerAllData> playerAllDataList, int roomIndex) {
        string mapName = string.Empty;
        switch (roomIndex) {
            case 0:
                mapName = "Forest";
                break;
        }

        SceneManager.LoadScene(mapName);

        StartCoroutine(CreatePlayer(playerAllDataList));
    }

    private IEnumerator CreatePlayer(List<PlayerAllData> playerAllDataList) {
        yield return null;

        foreach (PlayerAllData playerAllData in playerAllDataList) {
            PlayerController newPlayer = Instantiate(GameManager.Instance.PlayerControllerPrefab);
            newPlayer.transform.position = new Vector2(playerAllData.PosAndRot.X, playerAllData.PosAndRot.Y);
            newPlayer.transform.rotation = Quaternion.Euler(0, 0, playerAllData.PosAndRot.Rot);

            if (playerAllData.PlayerData.PlayerId == GameManager.Instance.PlayerInfo.PlayerId) {
                GameManager.Instance.PlayerInfo.PlayerController = newPlayer;
            }
        }
    }
}
