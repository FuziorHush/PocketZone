using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    public List<InventoryItem> Items { get; private set; } = new List<InventoryItem>();
    public int MaxCapacity { get; private set; }

    public Inventory(int maxCapacity)
    {
        MaxCapacity = maxCapacity;
    }

    public void SetMaxCapacity(int maxCapacity) {
        MaxCapacity = maxCapacity;

        if (maxCapacity < Items.Count)
        {
            for (int i = maxCapacity; i < Items.Count; i++)
                Items[i] = null;
        }
    }

    public bool PutItem(Item item, int amount)
    {
        if (item.Stacks)
        {
            InventoryItem itemToStack = Items.Find(x => x.Item.ID == item.ID);
            if (itemToStack != null)
            {
                itemToStack.Amount += amount;
            }
            else
            {
                if (Items.Count >= MaxCapacity)
                {
                    return false;
                }

                AddNewItem(item, amount);
            }
            return true;
        }
        else
        {
            if (Items.Count >= MaxCapacity)
            {
                return false;
            }

            AddNewItem(item, amount);
            return true;
        }
    }

    public void DeleteOneItem(int itemID)
    {
        if (MaxCapacity == 0)
            return;

        if (Items.Count > itemID && Items[itemID] != null)
        {
            if (Items[itemID].Amount > 1)
            {
                Items[itemID].Amount--;
            }
            else if (Items[itemID].Amount == 1)
            {
                Items.RemoveAt(itemID);
            }
        }
    }

    public void DeleteItemAll(int inventoryID)
    {
        if (MaxCapacity == 0)
            return;

        if (Items[inventoryID] != null)
        {
            Items.RemoveAt(inventoryID);
        }
    }

    private void AddNewItem(Item item, int amount)
    {
        InventoryItem newItem = new InventoryItem(item, amount);
        Items.Add(newItem);
    }
}
