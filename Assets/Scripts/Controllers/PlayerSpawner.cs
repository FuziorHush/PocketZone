using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _cameraPrefab;

    public void SpawnPlayer()
    {
        Transform playerSpawnPoint = SceneObjects.Instance.PlayerSpawnPoint;
        GameObject player = Instantiate(_playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        player.transform.SetParent(SceneObjects.Instance.CharactersParent);
        player.GetComponent<PlayerBase>().Init();
        SceneObjects.Instance.PlayerLink = player;

        GameObject camera = Instantiate(_cameraPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        camera.GetComponent<PlayerCameraFollow>().Init(player);
    }
}