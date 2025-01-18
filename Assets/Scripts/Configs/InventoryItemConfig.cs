using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItemConfig")]
public class InventoryItemConfig : ScriptableObject
{
    public ItemConfig ItemConfig;
    public int Amount;
}
