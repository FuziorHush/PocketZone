using UnityEngine;
using GameSave;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ItemsDatabase _itemsDatabase;
    [SerializeField] private PoolsController _poolsController;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private EnemiesSpawner _enemiesSpawner;
    [SerializeField] private ItemsDropSystem _itemsDropSystem;
    [SerializeField] private GameSaveController _gameSaveController;
    [SerializeField] private HUD _hud;

    private void Start()
    {
        _itemsDatabase.Init();
        _poolsController.Init();
        _itemsDropSystem.Init();

        if (!SaveLoadManager.CheckSaveFileExists())
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

        GameEvents.PlayerDied += Restart;
    }

    private void Restart() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
