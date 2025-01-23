using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : IPlayerContextUpdate
{
    public Inventory Inventory { get; private set; }

    private Transform _transform;
    private LayerMask _itemsLM;
    private float _pickupRadius;
    private PlayerShooting _playerShooting;

    public void Init(PlayerConfig config, Transform transform, PlayerShooting playerShooting, LayerMask itemsLM) 
    {
        _transform = transform;
        _playerShooting = playerShooting;
        _itemsLM = itemsLM;
        Inventory = new Inventory(config.InventoryCapacity);
        _pickupRadius = config.ItemsPickupRadius;
    }

    public void OnUpdate() 
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(_transform.position, _pickupRadius, _itemsLM);
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
                GameObject.Destroy(groundObject);
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
                    GameObject.Destroy(groundObject);
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
