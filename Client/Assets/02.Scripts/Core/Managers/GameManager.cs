using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Property
    public static GameManager Instance => _instance;
    #endregion
    private static GameManager _instance = null;


    [SerializeField] private string _url = string.Empty;


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
}
