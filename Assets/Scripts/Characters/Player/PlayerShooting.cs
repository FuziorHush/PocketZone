using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;

    private float _shootDelay;
    private float _bulletDamage;
    private float _bulletSpeed;

    private float _currentShootDelay;

    private IPlayerInputHandler _inputHandler;
    private GmObjectsPool _bulletsPool;

    public void Init(WeaponConfig weaponConfig, IPlayerInputHandler inputHandler)
    {
        _inputHandler = inputHandler;

        _shootDelay = weaponConfig.Shootdelay;
        _bulletDamage = weaponConfig.BulletDamage;
        _bulletSpeed = weaponConfig.BulletSpeed;
        _bulletsPool = PoolsController.Instance.GetPool(weaponConfig.BulletsPoolConfig);
    }

    void Update()
    {
        if (_currentShootDelay > 0) {
            _currentShootDelay -= Time.deltaTime;
        }

        if (_inputHandler.GetShooting())
        {
            if (_currentShootDelay > 0)
                return;

            ShootProjectile();
            _currentShootDelay += _shootDelay;
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = _bulletsPool.GetPooledObject();

        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.GetComponent<Bullet>().Init(_bulletDamage, _bulletSpeed);
            projectile.SetActive(true);
        }
    }
}
