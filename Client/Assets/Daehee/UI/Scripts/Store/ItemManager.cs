using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<ItemUI, ItemDataSO> _itemUIDic = new Dictionary<ItemUI, ItemDataSO>();

    [SerializeField]
    private List<ItemUI> _items;

    private ItemDataListSO _itemDataList;

    public static ItemManager Instance;

    [SerializeField]
    private int _itemAmount = 5;

    private int _curMoney;
    public int _CurMoney { get { return _curMoney; } set { _curMoney = value; } }

    [SerializeField]
    private GameObject _uiSlot;                                                                                                                                     
    private void Awake()
    {
        Instance = this;
        SetItemUI();
    }

    void SetItemUI()
    {
        _itemDataList = Resources.Load<ItemDataListSO>("UI/SO/dataList");
        for (int i = 0; i < _itemDataList._list.Count; i++)
        {
            _itemUIDic[_items[i]] = _itemDataList._list[i];
            Debug.Log(_itemDataList._list[i].name);
        }
    }

    public ItemDataSO GetSO(ItemUI ui)
    {
        return _itemUIDic[ui];
    }
}
