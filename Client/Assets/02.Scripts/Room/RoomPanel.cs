using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour {

    public Button Btn;
    public RoomInfo RoomInfo;
    public int Room;

    private void Start() {
        Btn = GetComponent<Button>();
        Btn.onClick.AddListener(() => {

        });
    }



}
