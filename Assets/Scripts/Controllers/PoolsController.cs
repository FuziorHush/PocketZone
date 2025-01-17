using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsController : MonoSingleton<PoolsController>
{
    private List<GmObjectsPool> _pools;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init() //Get all GmObjectsPool in childs
    {
        _pools = new List<GmObjectsPool>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out GmObjectsPool pool)) {
                _pools.Add(pool);
            }
        }
    }

    public GmObjectsPool GetPool(PoolConfig poolConfig)
    {
        return _pools.Find(x => x.PoolConfig == poolConfig);
    }
}
