using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmObjectsPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private PoolConfig _poolConfig;
    public PoolConfig PoolConfig => _poolConfig;

    private List<GameObject> pool;

    void Awake()
    {
        InitializePool();
    }

    private void InitializePool() //Instantiate GameObjects
    {
        pool = new List<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject() //Get 1st free pool object
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        //Make new if not enough objects
        GameObject obj = Instantiate(_prefab);
        obj.SetActive(false);
        pool.Add(obj);
        _poolSize++;
        return obj;
    }
}
