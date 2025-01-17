using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class InventoryItem
    {
        public InventoryItem(Item item, int amount)
        {
            if (item == null)
            {
                Debug.LogError("Item == null");
                return;
            }
            if (amount <= 0)
            {
                Debug.LogError("amount <= 0");
                return;
            }

            Item = item;
            Amount = amount;
        }

        public Item Item;
        public int Amount;

        public override string ToString()
        {
            return Item.Name;
        }
    }
}
