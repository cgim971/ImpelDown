using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Property
    public static GameManager Instance => _instance;

    public int PlayerId {
        get => _playerId;
        set => _playerId = value;
    }

    #endregion
    private static GameManager _instance = null;
    [SerializeField] private string _url = string.Empty;

    private int _playerId = -1;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Debug.LogError("Multiple GameManager is running!");
            Destroy(this.gameObject);
        }

        NetworkManager.Instance = gameObject.AddComponent<NetworkManager>();
        NetworkManager.Instance.Init(_url);
        NetworkManager.Instance.Connection();
    }

    public void GameStart(RoomInfo roomInfo) {
        StartCoroutine(GameStarting(roomInfo));
    }

    public IEnumerator GameStarting(RoomInfo roomInfo) {
        yield return null;
        SceneManager.LoadScene(Define.MapName(roomInfo.MapIndex));

        yield return null;

    }
}
