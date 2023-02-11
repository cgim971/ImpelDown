using ImpelDown.Proto;
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

    public int PlayerId {
        get => playerId;
        set {
            playerId = value;
        }
    }
    #endregion
    private static GameManager _instance = null;

    [SerializeField] private string _url = string.Empty;
    private int playerId = -1;


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
    }


    public void GameStart(RoomInfo roomInfo) {
        // 게임 시작시 맵 이동 후 플레이어 생성
        // 맵 이동
        SceneManager.LoadScene(Define.MapName(roomInfo.MapIndex));

        // 플레이어 생성
         
        // 플레이어에 카메라 달기

        //
    }


}