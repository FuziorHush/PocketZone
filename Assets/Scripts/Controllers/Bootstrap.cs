using UnityEngine;
using GameSave;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameFlowController _gameFlow; //restarting game
    [SerializeField] private ItemsDatabase _itemsDatabase; //db for items. configs taken from Resources
    [SerializeField] private PoolsController _poolsController; //inits GO pools and gives links to them
    [SerializeField] private PlayerSpawner _playerSpawner; //spawns player, camera
    [SerializeField] private EnemiesSpawner _enemiesSpawner; //spawns random enemies on random points
    [SerializeField] private ItemsDropSystem _itemsDropSystem; //spawwns items on top of dead enemies
    [SerializeField] private GameSaveController _gameSaveController; //loads and saves data
    [SerializeField] private HUD _hud;

    public static bool LoadSaveOnStart;

    static Bootstrap()
    {
        LoadSaveOnStart = true; //gameflow can set it false to start new game if save file exists
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
