using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemDataList")]
public class ItemDataListSO : ScriptableObject
{
    public List<ItemDataSO> _list;
}
