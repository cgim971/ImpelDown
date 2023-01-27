using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Button CreateBtn = null;
    public Button RefreshBtn = null;


    private void Start() {
        CreateBtn.onClick.AddListener(() => {
            RoomManager.Instance.CreateRoom();
        });

        RefreshBtn.onClick.AddListener(() => {
            RoomManager.Instance.RefreshRoom();
        });
    }

}
