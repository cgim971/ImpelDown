using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchManager : MonoBehaviour {

    #region Property
    public static MatchManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<MatchManager>();
            }
            return _instance;
        }
    }

    public RoomInPanelUI RoomInPanelUI { get; private set; }
    public RoomMapPanelUI RoomMapPanelUI { get; private set; }
    #endregion
    private static MatchManager _instance = null;


    [SerializeField] private CanvasGroup _roomInPanel;
    [SerializeField] private CanvasGroup _roomOutPanel;

    [SerializeField] private TMP_Text _nameText;


    private void Awake() {
        RoomInPanelUI = FindObjectOfType<RoomInPanelUI>();
        RoomMapPanelUI = FindObjectOfType<RoomMapPanelUI>();
    }

    public void Init(PlayerInfo playerInfo) {
        RoomOut();
        _nameText.SetText(playerInfo.PlayerName);
    }


    public void SetCanvasGroup(CanvasGroup canvasGroup, float alpha = 0f, bool interactable = false, bool blockRaycasts = false) {
        canvasGroup.alpha = alpha;
        canvasGroup.interactable = interactable;
        canvasGroup.blocksRaycasts = blockRaycasts;
    }


    public void RoomIn() {
        SetCanvasGroup(_roomOutPanel);
        SetCanvasGroup(_roomInPanel, 1f, true, true);
        RoomInPanelUI.Init();
    }

    public void RoomOut() {
        SetCanvasGroup(_roomInPanel);
        SetCanvasGroup(_roomOutPanel, 1f, true, true);
    }



}
