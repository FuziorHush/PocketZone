using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSave;
using Enemies;

public class EnemiesSpawner : MonoSingleton<EnemiesSpawner>
{
    [SerializeField] private GameObject[] _enemiesPrefabs;
    [SerializeField] private int _amountToSpawn;
    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        GameEvents.EnemyDied += OnEnemyDie;
    }

    public void SpawnEnemies() 
    {
        List<Transform> spawnPoints = new List<Transform>(SceneObjects.Instance.EnemiesSpawnPoints);
        for (int i = 0; i < _amountToSpawn; i++)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Count)];
            int enemyId = Random.Range(0, _enemiesPrefabs.Length);
            CreateEnemy(point.position, enemyId);
            spawnPoints.Remove(point);
        }
    }

    public EnemySaveData[] GetEnemiesData() 
    {
        EnemySaveData[] enemyDataArray = new EnemySaveData[_spawnedEnemies.Count];
        for (int i = 0; i < _spawnedEnemies.Count; i++)
        {
            GameObject enemy = _spawnedEnemies[i];
            CharacterSaveData characterData = new CharacterSaveData(new PositionSave(enemy.transform.position), enemy.GetComponent<Health>().CurrentHealth);
            enemyDataArray[i] = new EnemySaveData(characterData, enemy.GetComponent<EnemyBase>().Config.ID);
        }
        return enemyDataArray;
    }

    public void LoadEnemies(EnemySaveData[] enemiesData)
    {
        for (int i = 0; i < enemiesData.Length; i++)
        {
            Vector3 position = new Vector3(enemiesData[i].CharacterData.Position.X, enemiesData[i].CharacterData.Position.Y, enemiesData[i].CharacterData.Position.Z);
            GameObject enemy = CreateEnemy(position, enemiesData[i].ID);
            enemy.GetComponent<Health>().SetHealth(enemiesData[i].CharacterData.Health);
        }
    }

    private GameObject CreateEnemy(Vector3 position, int id) 
    {
        Transform charactersParent = SceneObjects.Instance.CharactersParent;
        GameObject enemy = Instantiate(_enemiesPrefabs[id], position, Quaternion.identity, charactersParent);
        enemy.GetComponent<EnemyBase>().Init();
        _spawnedEnemies.Add(enemy);
        return enemy;
    }

    private void OnEnemyDie(GameObject enemy)
    {
        _spawnedEnemies.Remove(enemy);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        GameEvents.EnemyDied -= OnEnemyDie;
    }
}
