using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PoolsController _poolsController;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private EnemiesSpawner _enemiesSpawner;

    private void Start()
    {
        _poolsController.Init();

        _playerSpawner.SpawnPlayer();
        _enemiesSpawner.SpawnEnemies();
    }
}
