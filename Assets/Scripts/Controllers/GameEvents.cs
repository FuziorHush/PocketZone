using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    public static UnityAction PlayerDied;
    public static UnityAction<GameObject> EnemyDied;
    public static UnityAction<Inventory> PlayerInventoryUpdated;
    public static UnityAction<InventoryItem> ItemPickedUp;
    public static UnityAction<int> PlayerAmmoAmountChanged;
}
