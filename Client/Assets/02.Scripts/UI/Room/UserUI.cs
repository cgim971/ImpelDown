using ImpelDown.Proto;
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
    private Image _playerImage;
    private TMP_Text _nameText;
    private Button _lockBtn;

    private bool _isLock = false;
    private int _roomIndex = -1;

    public void Init(int roomInIndex) {
        _image = transform.Find("userImage").GetComponent<Image>();
        _playerImage = transform.GetComponent<Image>();
        _nameText = transform.Find("nameText").GetComponent<TMP_Text>();
        _lockBtn = GetComponent<Button>();
        _lockBtn.onClick.AddListener(() => LockBtn());

        _roomIndex = roomInIndex;
    }

    public void Refresh(Sprite playerSprite, bool isPlayer = false, bool isLock = false, string playerName = "") {
        _image.gameObject.SetActive(isPlayer);
        _playerImage.sprite = playerSprite;
        _isLock = isLock;
        _nameText.SetText(playerName);
    }

    public void LockBtn() {
        if (MatchManager.Instance.RoomInPanelUI.IsHost == false)
            return;

        C_IsLock data = new C_IsLock { PlayerId = GameManager.Instance.PlayerId, IsLock = !_isLock, RoomInIndex = _roomIndex };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CIslock, data);
    }

}
