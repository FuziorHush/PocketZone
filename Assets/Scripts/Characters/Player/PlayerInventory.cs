using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory Inventory { get; private set; }

    [SerializeField] private LayerMask _itemsLM;

    private float _pickupRadius;

    private PlayerShooting _playerShooting;

    public void Init(PlayerConfig config) 
    {
        _playerShooting = GetComponent<PlayerShooting>();
        Inventory = new Inventory(config.InventoryCapacity);
        _pickupRadius = config.ItemsPickupRadius;
    }

    private void Update()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, _pickupRadius, _itemsLM);
        for (int i = 0; i < items.Length; i++)
        {
            PickupItem(items[i].gameObject);
        }
    }

    public bool PickupItem(GameObject groundObject)
    {
        InventoryItem inventoryItem = groundObject.GetComponent<GroundItem>().InventoryItem;
        Item item = inventoryItem.Item;
        if (inventoryItem.Item.PutsIntoSlot)
        {
            if (Inventory.PutItem(item, inventoryItem.Amount))
            {
                Destroy(groundObject);
                GameEvents.ItemPickedUp?.Invoke(inventoryItem);
                GameEvents.PlayerInventoryUpdated?.Invoke(Inventory);
                return true;
            }
        }
        else
        {
            switch (item.Name)
            {
                case "ammo":
                    _playerShooting.AddAmmo(inventoryItem.Amount);
                    Destroy(groundObject);
                    GameEvents.ItemPickedUp?.Invoke(inventoryItem);
                    return true;
            }
        }

        return false;
    }

    public void DeleteItem(int slotID)
    {
        Inventory.DeleteOneItem(slotID);
    }
}
