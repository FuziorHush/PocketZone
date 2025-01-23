using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private WeaponConfig _weaponConfig;
    [SerializeField] private GameObject _playerBody;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LayerMask _itemsLM;
    [SerializeField] private LayerMask _enemyDetectionCircleCastLM;
    [SerializeField] private LayerMask _enemyDetectionOverlapCircleLM;
    [SerializeField] private GameObject _targetMarkPrefab;

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerShooting PlayerShooting { get; private set; }
    public PlayerInventory PlayerInventory { get; private set; }
    public PlayerEnemiesDetection PlayerEnemiesDetection { get; private set; }
    public TargetMarkController TargetMark { get; private set; }
    public Health HealthConponent { get; private set; }
    public IPlayerInputHandler InputHandler { get; private set; }

    private List<IPlayerContextUpdate> _updateContexts = new List<IPlayerContextUpdate>();
    private List<IPlayerContextFixedUpdate> _fixedUpdateContexts = new List<IPlayerContextFixedUpdate>();

    public void Init()
    {
        PlayerMovement = new PlayerMovement();
        _updateContexts.Add(PlayerMovement);
        _fixedUpdateContexts.Add(PlayerMovement);

        PlayerShooting = new PlayerShooting();
        _updateContexts.Add(PlayerShooting);

        PlayerInventory = new PlayerInventory();
        _updateContexts.Add(PlayerInventory);

        PlayerEnemiesDetection = new PlayerEnemiesDetection();
        _updateContexts.Add(PlayerEnemiesDetection);

        TargetMark = new TargetMarkController();
        _updateContexts.Add(TargetMark);

        InputHandler = new PlayerInputHandlerMobile();
        InputHandler.Init(gameObject);

        HealthConponent = GetComponent<Health>();
        HealthConponent.Init(_playerConfig.Health);
        HealthConponent.HealthChanged += OnHealthChanged;

        PlayerMovement.Init(_playerConfig, InputHandler, GetComponent<Rigidbody2D>(), _playerBody.GetComponent<Animator>());
        PlayerShooting.Init(_weaponConfig, InputHandler, PlayerEnemiesDetection, _shootPoint);
        PlayerInventory.Init(_playerConfig, transform, PlayerShooting, _itemsLM);
        PlayerEnemiesDetection.Init(_playerConfig, _enemyDetectionCircleCastLM, _enemyDetectionOverlapCircleLM, _shootPoint);
        TargetMark.Init(PlayerEnemiesDetection, _targetMarkPrefab);
    }

    private void Update()
    {
        for (int i = 0; i < _updateContexts.Count; i++)
        {
            _updateContexts[i].OnUpdate();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _fixedUpdateContexts.Count; i++)
        {
            _fixedUpdateContexts[i].OnFixedUpdate();
        }
    }

    private void OnHealthChanged(float health, float maxHealth)
    {
        if (health <= 0)
        {
            GameEvents.PlayerDied?.Invoke();
        }
    }
}
