using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserUI : MonoBehaviour {
    public void Init() { }

    public void Refresh(string playerName = "") {
        TMP_Text text =  transform.Find("nameText").GetComponent<TMP_Text>();
        text.SetText(playerName);
    }

}
