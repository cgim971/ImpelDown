using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour {
    #region Property
    public bool IsLock {
        get => _isLock;
        set => _isLock = value;
    }
    #endregion

    private Image _image;
    private TMP_Text _nameText;

    private bool _isLock = false;

    public void Init() {
        _image = transform.Find("userImage").GetComponent<Image>();
        _nameText = transform.Find("nameText").GetComponent<TMP_Text>();
    }

    public void Refresh(Sprite playerSprite, bool isPlayer = false, string playerName = "") {
        GetComponent<Image>().sprite = playerSprite;
        _nameText.SetText(playerName);

        _image.gameObject.SetActive(isPlayer);
    }

}
