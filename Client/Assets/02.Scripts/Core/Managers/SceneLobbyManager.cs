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


    [Header("Room List Manager")]
    public GameObject RoomPanel = null;
    public Transform Content = null;

    [Space(10f)]
    public Button CreateRoomBtn = null;
    public Button RefreshRoomListBtn = null;
    public Button ExitRoomBtn = null;

    [Space(10f)]
    public GameObject RoomInPanel = null;
    public GameObject RoomOutPanel = null;

    public GameObject StartBtn = null;
    public Text Text = null;


    private void Awake() {
        if (_instance != null)
            Debug.LogError("Multiple SceneLobbyManager is running!");
        _instance = this;

        Init();
    }

    private void Init() {
        RoomListManager.Instance = gameObject.AddComponent<RoomListManager>();
        RoomListManager.Instance.Init(RoomPanel, Content);

        CreateRoomBtn.onClick.AddListener(() => RoomListManager.Instance.CreateRoom());
        RefreshRoomListBtn.onClick.AddListener(() => RoomListManager.Instance.RefreshRoom());
        ExitRoomBtn.onClick.AddListener(() => RoomListManager.Instance.ExitRoom());

        StartBtn.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.StartGame());
    }
}
