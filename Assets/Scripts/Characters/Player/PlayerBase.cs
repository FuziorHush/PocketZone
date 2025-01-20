using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private WeaponConfig _weaponConfig;

    private PlayerMovement _playerMovement;
    private PlayerShooting _playerShooting;
    private PlayerInventory _playerInventory;
    private PlayerEnemiesDetection _playerEnemiesDetection;
    private TargetMarkController _targetMark;
    private Health _healthConponent;
    private IPlayerInputHandler _inputHandler;

    public void Init()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShooting = GetComponent<PlayerShooting>();
        _playerInventory = GetComponent<PlayerInventory>();
        _playerEnemiesDetection = GetComponent<PlayerEnemiesDetection>();
        _targetMark = GetComponent<TargetMarkController>();
        _healthConponent = GetComponent<Health>();

        _inputHandler = new PlayerInputHandlerMobile();
        _inputHandler.Init(gameObject);

        _healthConponent.Init(_playerConfig.Health);
        _healthConponent.HealthChanged += OnHealthChanged;

        _playerMovement.Init(_playerConfig, _inputHandler);
        _playerShooting.Init(_weaponConfig, _inputHandler);
        _playerInventory.Init(_playerConfig);
        _playerEnemiesDetection.Init();
        _targetMark.Init();
    }

    private void OnHealthChanged(float health, float maxHealth)
    {
        if (health <= 0)
        {
            GameEvents.PlayerDied?.Invoke();
        }
    }
}
