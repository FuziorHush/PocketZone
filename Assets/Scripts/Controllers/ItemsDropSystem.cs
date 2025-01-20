using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDropSystem : MonoSingleton<ItemsDropSystem>
{
    [SerializeField] private GameObject _groundItemPrefab;
    private Transform _itemsParent;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        _itemsParent = SceneObjects.Instance.ItemsParent;
        GameEvents.EnemyDied += OnEnemyDeath;
    }

    private void OnEnemyDeath(GameObject enemy) 
    {
        Vector3 deathPos = enemy.transform.position;
        ItemsDropConfig itemsDropConfig = enemy.GetComponent<EnemyBase>().Config.ItemsDropConfig;
        InventoryItemConfig itemConfig = itemsDropConfig.ItemsToDrop[Random.Range(0, itemsDropConfig.ItemsToDrop.Length)];
        GameObject droppedItem = Instantiate(_groundItemPrefab, deathPos, Quaternion.identity, _itemsParent);
        droppedItem.GetComponent<GroundItem>().Init(itemConfig);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        GameEvents.EnemyDied -= OnEnemyDeath;
    }
}
