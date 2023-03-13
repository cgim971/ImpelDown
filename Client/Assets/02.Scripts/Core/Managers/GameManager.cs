using Google.Protobuf.Collections;
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

    public BasePlayer HunterPlayer;

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

    public void GameStart(int mapIndex, RepeatedField<PlayerInitData> playerDatas) {
        StartCoroutine(GameStarting(mapIndex, playerDatas));
    }

    public IEnumerator GameStarting(int mapIndex, RepeatedField<PlayerInitData> playerDatas) {
        yield return null;
        SceneManager.LoadScene(Define.MapName(mapIndex));

        yield return null;
        PlayerManager.Instance = new PlayerManager();
        PlayerManager.Instance.CreatePlayer(playerDatas);
    }
}
