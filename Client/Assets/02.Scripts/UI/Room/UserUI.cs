using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour {
    private Image _image;
    private TMP_Text _nameText;

    public void Init() {
        _image = transform.Find("image").GetComponent<Image>();
        _nameText = transform.Find("nameText").GetComponent<TMP_Text>();
    }

    public void Refresh(Sprite playerSprite, bool isPlayer = false, string playerName = "") {
        GetComponent<Image>().sprite = playerSprite;
        _nameText.SetText(playerName);

        _image.gameObject.SetActive(isPlayer);
    }

}
