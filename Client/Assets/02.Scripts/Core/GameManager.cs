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
    #endregion
    private static GameManager _instance = null;


    [SerializeField] private string _url = string.Empty;

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
            C_Match_Making cMatchMaking= new C_Match_Making { PlayerId = 0 };
            NetworkManager.Instance.RegisterSend((ushort)MSGID.CMatchMaking, cMatchMaking);
        }
    }

}
    