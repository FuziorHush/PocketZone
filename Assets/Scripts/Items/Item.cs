using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Item(int id, string name, bool stacks, bool putsIntoSlot, Sprite icon)
    {
        ID = id;
        Name = name;
        Stacks = stacks;
        PutsIntoSlot = putsIntoSlot;
        Icon = icon;
    }

    public int ID;

    public string Name;
    public bool Stacks;
    public bool PutsIntoSlot; //false - item does not puts into inventory and has special actions after pickup. (add ammo, heal...)
    public Sprite Icon;
}
