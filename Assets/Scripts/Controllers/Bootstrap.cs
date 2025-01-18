using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PoolsController _poolsController;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private EnemiesSpawner _enemiesSpawner;
    [SerializeField] private ItemsDropSystem _itemsDropSystem;

    private void Start()
    {
        _poolsController.Init();
        _itemsDropSystem.Init();

        _playerSpawner.SpawnPlayer();
        _enemiesSpawner.SpawnEnemies();
    }
}
