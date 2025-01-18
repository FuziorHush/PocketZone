using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemConfig")]
public class ItemConfig : ScriptableObject
{
    public int ID;

    public string Name;
    public bool Stacks;
    public bool PutsInSlot; //false - item does not puts into inventory and has special actions after pickup. (add ammo, heal...)
    public Sprite Icon;
}
