using UnityEngine;
using GameSave;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameFlowController _gameFlow;
    [SerializeField] private ItemsDatabase _itemsDatabase;
    [SerializeField] private PoolsController _poolsController;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private EnemiesSpawner _enemiesSpawner;
    [SerializeField] private ItemsDropSystem _itemsDropSystem;
    [SerializeField] private GameSaveController _gameSaveController;
    [SerializeField] private HUD _hud;

    public static bool LoadSaveOnStart;

    static Bootstrap()
    {
        LoadSaveOnStart = true;
    }

    private void Start()
    {
        _gameFlow.Init();
        _itemsDatabase.Init();
        _poolsController.Init();
        _itemsDropSystem.Init();

        if (!SaveLoadManager.CheckSaveFileExists() || !LoadSaveOnStart)
        {
            _playerSpawner.SpawnPlayer();
            _hud.Init();
            _enemiesSpawner.SpawnEnemies();
        }
        else 
        {
            GameSaveData gameSave = GameSaveController.Instance.LoadSave();
            _playerSpawner.LoadPlayer(gameSave.PlayerData);
            _hud.Init();
            _enemiesSpawner.LoadEnemies(gameSave.EnemyData);
        }

        LoadSaveOnStart = true;
    }
}
