using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemsDropConfig")]
public class ItemsDropConfig : ScriptableObject
{
    public Item[] ItemsToDrop;
}
