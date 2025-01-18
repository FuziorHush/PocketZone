using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemiesPrefabs;
    [SerializeField] private int _amountToSpawn;

    public void SpawnEnemies() 
    {
        Transform charactersParent = SceneObjects.Instance.CharactersParent;
        List<Transform> spawnPoints = new List<Transform>(SceneObjects.Instance.EnemiesSpawnPoints);
        for (int i = 0; i < _amountToSpawn; i++)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject enemy = Instantiate(_enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Length)], point.position, point.rotation, charactersParent);
            enemy.GetComponent<EnemyBase>().Init();
            spawnPoints.Remove(point);
        }
    }
}
