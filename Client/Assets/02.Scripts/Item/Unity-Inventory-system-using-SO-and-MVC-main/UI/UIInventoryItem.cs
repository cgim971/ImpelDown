using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;

        private bool empty = true;

        public void Awake()
        {
            ResetData();
        }
        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            empty = true;
        }
        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            empty = false;
        }
    }
}