using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string _itemName;
    public int _itemCost;
    public Sprite _icon;
    public string _skillInfo;
    public string _backGroundInfo;
    public bool _isHave;
}
