using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase : MonoSingleton<ItemsDatabase>
{
    private string _itemsConfigsPath = "Configs/Items";

    private List<ItemConfig> _configs;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init() 
    {
        _configs = new List<ItemConfig>(Resources.LoadAll<ItemConfig>(_itemsConfigsPath));
    }

    public ItemConfig GetItemConfigByID(int id)
    {
        return _configs.Find(x => x.ID == id);
    }
}
