using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjects : MonoSingleton<SceneObjects>
{
    [HideInInspector] public GameObject PlayerGameObject;
    [HideInInspector] public PlayerBase PlayerBase;

    public Transform PlayerSpawnPoint;
    public Transform[] EnemiesSpawnPoints;

    public Transform CharactersParent;
    public Transform ItemsParent;
}
