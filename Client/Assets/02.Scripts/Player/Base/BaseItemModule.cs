using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseItemModule : MonoBehaviour {
    #region Property
    public BasePlayer Player => _player;
    #endregion

    protected BasePlayer _player;

    [SerializeField]
    private UIInventoryPage inventoryUI;
    [SerializeField]
    private InventorySO inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    public virtual void Init()
    {
        inventoryUI = GameObject.Find("ItemInventory").GetComponent<UIInventoryPage>();
        inventoryData = Resources.Load<InventorySO>("SO/NewInventorySO");
        PrepareUI();
        PrepareInventoryData();
        inventoryUI.Show();
    }

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach (InventoryItem item in initialItems)
        {
            if (item.IsEmpty)
                continue;
            inventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage,
                item.Value.quantity);
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject);
        }
    }

    public void UpdateItem()
    {
        foreach (var item in inventoryData.GetCurrentInventoryState())
        {
            inventoryUI.UpdateData(item.Key,
                item.Value.item.ItemImage,
                item.Value.quantity);
        }
    }

    public void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.T) == false)
            return;

        if (!inventoryData.GetItemAt(0).IsEmpty)
        {
            PerformAction(0);
        }
    }

    private float time = 0f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            Debug.Log(time);
            time += Time.deltaTime;
            if (time >= 4f)
            {
                time = 0;
                int rand = Random.Range(0, item.InventoryItem.Count);
                inventoryData.RemoveItem(0, 1);
                inventoryData.AddItem(item.InventoryItem[rand], item.Quantity);
                item.DestroyItem();
                UpdateItem();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            time = 0;
        }
    }


}
