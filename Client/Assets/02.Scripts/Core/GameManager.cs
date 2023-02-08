using ImpelDown.Proto;
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

    public int PlayerId {
        get => _playerId;
        set => _playerId = value;
    }
    #endregion
    private static GameManager _instance = null;

    [SerializeField] private string _url = string.Empty;

    private int _playerId = -1;

    private void Awake() {
        if (_instance != null)
            Debug.LogError("Multiple GameManager is running!");

        _instance = this;

        NetworkManager.Instance = gameObject.AddComponent<NetworkManager>();
        NetworkManager.Instance.Init(_url);
        NetworkManager.Instance.Connection();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {

        }
    }

}
