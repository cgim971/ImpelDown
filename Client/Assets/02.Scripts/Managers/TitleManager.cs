using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {
    #region Property
    public static TitleManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<TitleManager>();
            }
            return _instance;
        }
    }
    #endregion
    private static TitleManager _instance = null;

    [SerializeField] private TMP_InputField _inputFieldName;
    [SerializeField] private Button _okBtn;

    private void Start() {
        _okBtn.onClick.AddListener(() => Name());
    }

    public void Name() {
        string name = _inputFieldName.text;
        C_Player data = new C_Player { PlayerId = GameManager.Instance.PlayerId, PlayerName = name };
        NetworkManager.Instance.RegisterSend((ushort)MSGID.CPlayer, data);
    }

    public void ClosePanel() {
        gameObject.SetActive(false);
    }
}