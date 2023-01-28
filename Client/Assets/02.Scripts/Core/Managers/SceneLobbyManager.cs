using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLobbyManager : MonoBehaviour {

    #region Property
    public static SceneLobbyManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<SceneLobbyManager>();

            return _instance;
        }
    }
    #endregion
    private static SceneLobbyManager _instance = null;


    public GameObject RoomPanel = null;
    public Transform Content = null;
    public Button CreateRoomBtn = null;
    public Button RefreshRoomListBtn = null;
    public Button ExitRoomBtn = null;


    private void Awake() {
        if (_instance != null)
            Debug.LogError("Multiple SceneLobbyManager is running!");
        _instance = this;

        Init();
    }

    private void Init() {
        RoomListManager.Instance = gameObject.AddComponent<RoomListManager>();
        RoomListManager.Instance.Init(RoomPanel, Content, CreateRoomBtn, RefreshRoomListBtn, ExitRoomBtn);
    }
}
