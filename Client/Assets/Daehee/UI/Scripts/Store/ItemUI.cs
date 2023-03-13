using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ItemUI : MonoBehaviour
{
    private ItemDataSO _itemDataSO;

    #region itemData
    [SerializeField]
    private TMP_Text _name;

    [SerializeField]
    private TMP_Text _cost;

    [SerializeField]
    private TMP_Text _skillInfo;

    [SerializeField]
    private TMP_Text _backgroundInfo;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private bool isHaving = false;
    #endregion

    [SerializeField]
    private Button _changeBtn;

    public Button _buyBtn;

    public List<Image> _changeImageList;
    private bool isIcon = true;

    private Sequence _seq;
    void Start()
    {
        _itemDataSO = ItemManager.Instance.GetSO(this);
        _name.text = _itemDataSO.name;
        _cost.text = _itemDataSO._itemCost.ToString();
        _skillInfo.text = _itemDataSO._skillInfo;
        _backgroundInfo.text = _itemDataSO._backGroundInfo;
        _icon.sprite = _itemDataSO._icon;
        isHaving = _itemDataSO._isHave;
        _changeBtn.onClick.AddListener(ChangeButton);
        ChangeButton();
        _buyBtn.onClick.AddListener(ItemBuy);
    }

    void ItemBuy()
    {
        Debug.Log("clk");
        int _curMoney = ItemManager.Instance._CurMoney;//현재 가지고 있는 돈 저장
        int _itemCost = int.Parse(_cost.text);
        if (_itemCost > _curMoney)
        {

            return;
        }
        else
        {
            ItemManager.Instance._CurMoney -= _itemCost;
            isHaving = true;
        }
    }


    void ChangeButton()
    {
        _changeBtn.enabled = false;
        isIcon = !isIcon;
        _seq = DOTween.Sequence();
        if (isIcon)
        {
            _seq.Append(_changeImageList[0].DOFade(0, 0.5f));
            _seq.Join(_changeImageList[1].DOFade(1, 0.5f));
        }
        else
        {
            _seq.Append(_changeImageList[1].DOFade(0, 0.5f));
            _seq.Join(_changeImageList[0].DOFade(1, 0.5f));
        }
        _seq.AppendCallback(() =>
        {
            _seq.Kill();
            _changeBtn.enabled = true;
        });
    }
}
