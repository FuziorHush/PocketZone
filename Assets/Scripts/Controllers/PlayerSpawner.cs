using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    public void SpawnPlayer()
    {
        Transform playerSpawnPoint = SceneObjects.Instance.PlayerSpawnPoint;
        Instantiate(_playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation).transform.SetParent(SceneObjects.Instance.CharactersParent);
    }
}