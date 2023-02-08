using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbySceneManager : MonoBehaviour {
    #region Property
    public static LobbySceneManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<LobbySceneManager>();

            return _instance;
        }
    }
    #endregion
    private static LobbySceneManager _instance = null;

    [SerializeField] private Button _matchMakingBtn;
    [SerializeField] private Button _refreshRoomListBtn;
    [SerializeField] private Button _createRoomBtn;
    [SerializeField] private InputField _maxPeopleField;


    private void Awake() {
        Init();
    }

    void Init() {
        _matchMakingBtn.onClick.AddListener(() => MatchManager.Instance.MatchMaking());
        _refreshRoomListBtn.onClick.AddListener(() => MatchManager.Instance.RefreshRoomList());
        _createRoomBtn.onClick.AddListener(() => MatchManager.Instance.CreateRoom(int.Parse(_maxPeopleField.text.ToString())));
    }

}
