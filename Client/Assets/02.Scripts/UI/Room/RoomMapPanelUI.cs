using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMapPanelUI : MonoBehaviour {

    [SerializeField] private Button _leftBtn;
    [SerializeField] private Button _rightBtn;
    [SerializeField] private Image _mapImage;


    private void Start() {
        _leftBtn.onClick.AddListener(() => LeftBtn());
        _rightBtn.onClick.AddListener(() => RightBtn());
    }

    public void Init(bool isHost, int mapIndex) {
        if (isHost) {
            ShowBtns();
        }
        else {
            UnshownBtns();
        }

        switch (mapIndex) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    public void ShowBtns() {
        _leftBtn.gameObject.SetActive(true);
        _rightBtn.gameObject.SetActive(true);
    }

    public void UnshownBtns() {
        _leftBtn.gameObject.SetActive(false);
        _rightBtn.gameObject.SetActive(false);
    }




    public void LeftBtn() {

    }

    public void RightBtn() {

    }



}
