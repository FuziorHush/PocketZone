using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : IPlayerContextUpdate
{
    private float _shootDelay;
    private float _bulletDamage;
    private float _bulletSpeed;
    private float _bulletDistance;

    private int _ammo;
    public int Ammo => _ammo;

    private float _currentShootDelay;

    private IPlayerInputHandler _inputHandler;
    private PlayerEnemiesDetection _playerDetection;
    private GmObjectsPool _bulletsPool;
    private Transform _shootPoint;

    public void Init(WeaponConfig weaponConfig, IPlayerInputHandler inputHandler, PlayerEnemiesDetection playerDetection, Transform shootPoint)
    {
        _inputHandler = inputHandler;
        _playerDetection = playerDetection;
        _shootPoint = shootPoint;

        _shootDelay = weaponConfig.Shootdelay;
        _bulletDamage = weaponConfig.BulletDamage;
        _bulletSpeed = weaponConfig.BulletSpeed;
        _bulletDistance = weaponConfig.Distance;
        _ammo = weaponConfig.Ammo;
        _bulletsPool = PoolsController.Instance.GetPool(weaponConfig.BulletsPoolConfig);
    }

    public void AddAmmo(int amount)
    {
        _ammo += amount;
        GameEvents.PlayerAmmoAmountChanged?.Invoke(_ammo);
    }

    public void SetAmmo(int amount)
    {
        _ammo = amount;
        GameEvents.PlayerAmmoAmountChanged?.Invoke(_ammo);
    }

    public void OnUpdate() 
    {
        if (_playerDetection.TargetEnemy != null)
        {
            RotateShootPointToTarget();
        }

        if (_currentShootDelay > 0)
        {
            _currentShootDelay -= Time.deltaTime;
        }

        if (_inputHandler.GetShooting())
        {
            if (_currentShootDelay > 0 || _ammo <= 0)
                return;

            ShootProjectile();
            _ammo--;
            GameEvents.PlayerAmmoAmountChanged?.Invoke(_ammo);
            _currentShootDelay += _shootDelay;
        }
    }

    private void RotateShootPointToTarget()
    {
        Vector3 lookVector = _playerDetection.TargetEnemy.position - _shootPoint.position;
        float rotationZ = Mathf.Atan2(lookVector.y, lookVector.x) * Mathf.Rad2Deg;
        _shootPoint.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
    }

    private void ShootProjectile()
    {
        GameObject projectile = _bulletsPool.GetPooledObject();

        if (projectile != null)
        {
            projectile.transform.position = _shootPoint.position;
            projectile.transform.rotation = _shootPoint.rotation;
            projectile.GetComponent<Bullet>().Init(_bulletDamage, _bulletSpeed, _bulletDistance);
            projectile.SetActive(true);
        }
    }
}
