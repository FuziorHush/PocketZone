using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public InventoryItem InventoryItem { get; private set; }

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Init(InventoryItemConfig inventoryItemConfig)
    {
        ItemConfig itemConfig = inventoryItemConfig.ItemConfig;
        Item item = new Item(itemConfig.ID, itemConfig.Name, itemConfig.Stacks, itemConfig.PutsInSlot, itemConfig.Icon);
        InventoryItem = new InventoryItem(item, inventoryItemConfig.Amount);
        _spriteRenderer.sprite = itemConfig.Icon;
    }
}
