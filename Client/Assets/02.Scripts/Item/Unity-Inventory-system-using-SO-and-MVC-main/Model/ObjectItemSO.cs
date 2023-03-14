using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ObjectItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<ObjectData> modifiersData = new List<ObjectData>();

        public string ActionName => "Object";

        public bool PerformAction(GameObject character)
        {
            foreach (ObjectData data in modifiersData)
            {
                data.useObjecter.UseObject(character, data.item, data.value);
            }
            return true;
        }
    }

    [Serializable]
    public class ObjectData
    {
        public CharacterUseObjectSO useObjecter;
        public GameObject item;
        public float value;
    }
}