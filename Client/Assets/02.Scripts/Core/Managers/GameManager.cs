using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Property
    public static GameManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }

    public PlayerInfo PlayerInfo => _playerInfo;
    #endregion
    private static GameManager _instance = null;

    [SerializeField] private string _url = string.Empty;

    private PlayerInfo _playerInfo;

    private void Awake() {
        if (_instance != null)
            Debug.LogError("Multiple GameManager is running!");
        _instance = this;
        DontDestroyOnLoad(_instance);

        Init();
    }

    private void Init() {
        NetworkManager.Instance = gameObject.AddComponent<NetworkManager>();
        NetworkManager.Instance.Init(_url);
        NetworkManager.Instance.Connection();

        RoomManager.Instance = gameObject.AddComponent<RoomManager>();
        RoomManager.Instance.Init();
    }

    public PlayerInfo SetPlayer() {
        PlayerInfo newPlayerInfo = new PlayerInfo();
        _playerInfo = newPlayerInfo; 
        return _playerInfo;
    }

    private void OnApplicationQuit() {
        RoomListManager.Instance.ExitRoom();
    }
}