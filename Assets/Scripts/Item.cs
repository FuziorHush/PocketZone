using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Item(int id, string name, bool stacks, Sprite icon)
    {
        ID = id;
        Name = name;
        Stacks = stacks;
        Icon = icon;
    }

    public int ID;

    public string Name;
    public bool Stacks;
    public bool InventoryItem; //false - item does not puts into inventory and has special actions after pickup. (add ammo, heal...)
    public Sprite Icon;
}
