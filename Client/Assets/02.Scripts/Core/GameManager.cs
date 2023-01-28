using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Property
    public static GameManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }
    public PlayerController PlayerController => _playerController;

    #endregion
    private static GameManager _instance = null;

    [SerializeField] private string _url = string.Empty;

    [SerializeField] public PlayerController _playerControllerPrefab;
    private PlayerController _playerController;

    public GameObject RoomPanel;
    public Transform Content;


    private void Awake() {
        if (_instance != null)
            Debug.LogError("Multiple GameManager is running!");

        _instance = this;

        NetworkManager.Instance = gameObject.AddComponent<NetworkManager>();
        NetworkManager.Instance.Init(_url);
        NetworkManager.Instance.Connection();

        RoomManager.Instance = gameObject.AddComponent<RoomManager>();
        RoomManager.Instance.Init(RoomPanel, Content);
    }

    public PlayerController SetPlayer() {
        _playerController = Instantiate(_playerControllerPrefab);
        return _playerController;
    }
}