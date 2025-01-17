using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _raycastRadius;
    [SerializeField] private PoolConfig _destroyEffectPoolConfig;

    private GmObjectsPool _destroyEffectPool;
    private Vector3 _previousPos;
    private float _damage;
    private float _speed;

    public void Init(float damage, float speed)
    {
        _damage = damage;
        _speed = speed;

        if (_destroyEffectPool == null) 
        {
            _destroyEffectPool = PoolsController.Instance.GetPool(_destroyEffectPoolConfig);
        }
    }

    void FixedUpdate()
    {
        _previousPos = transform.position;
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        Vector2 delta = transform.position - _previousPos;
        RaycastHit2D hit = Physics2D.CircleCast(_previousPos, _raycastRadius, delta.normalized, delta.magnitude, _layerMask);

        if (hit.transform != null)
        {
            if (hit.transform.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }

            SpawnDestroyEffect();

            gameObject.SetActive(false);
        }
    }

    private void SpawnDestroyEffect() 
    {
        GameObject effect = _destroyEffectPool.GetPooledObject();
        if (effect != null)
        {
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
            effect.SetActive(true);
        }
    }
}
