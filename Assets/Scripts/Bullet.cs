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
    private float _currentDistance;

    private float _damage;
    private float _speed;
    private float _distance;

    public void Init(float damage, float speed, float distance)
    {
        _damage = damage;
        _speed = speed;
        _distance = distance;
        _currentDistance = 0;

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

            SpawnDestroyEffect(hit.point);
            gameObject.SetActive(false);
        }

        _currentDistance += delta.magnitude;
        if (_currentDistance >= _distance) 
        {
            SpawnDestroyEffect(transform.position);
            gameObject.SetActive(false);
        }
    }

    private void SpawnDestroyEffect(Vector3 position) 
    {
        GameObject effect = _destroyEffectPool.GetPooledObject();
        if (effect != null)
        {
            effect.transform.position = position;
            effect.transform.rotation = transform.rotation;
            effect.SetActive(true);
        }
    }
}
