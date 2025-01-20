using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoSingleton<HUD>
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private HeldButton _shootButton;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private RectTransform _deleteItemRectTransform;
    [SerializeField] private Button _deleteItemButton;
    [SerializeField] private Text _ammoText;
    [SerializeField] private InventoryButton[] _inventoryButtons;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _restartButton;

    private PlayerInventory _playerInventory;

    private int _slotIdToDeleteItem = -1;
    private float _deleteItemRectYOffset = 100f;

    public UnityAction<int> SendDeleteItem;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        GameObject player = SceneObjects.Instance.PlayerLink;

        _playerInventory = player.GetComponent<PlayerInventory>();
        _ammoText.text = player.GetComponent<PlayerShooting>().Ammo.ToString();

        _deleteItemButton.onClick.AddListener(delegate { InventoryDeleteButtonPressed(_slotIdToDeleteItem); });
        _inventoryButton.onClick.AddListener(SwitchInventory);

        _saveButton.onClick.AddListener(SaveGame);
        _loadButton.onClick.AddListener(LoadGame);
        _restartButton.onClick.AddListener(RestartGame);

        GameEvents.PlayerAmmoAmountChanged += OnPlayerAmmoAmountChange;
        GameEvents.PlayerInventoryUpdated += UpdateInventory;

        for (int i = 0; i < _inventoryButtons.Length; i++)
        {
            _inventoryButtons[i].Init(this, i);
        }
    }

    public Vector2 GetJoystickAxis() 
    {
        return new Vector2(_joystick.Horizontal, _joystick.Vertical);
    }

    public bool GetShootButton()
    {
        return _shootButton.isPressed;
    }

    public void InventoryClotButtonPressed(int id) 
    {
        if (_slotIdToDeleteItem != id)
        {
            _deleteItemRectTransform.gameObject.SetActive(true);
            RectTransform rectTransform = _inventoryButtons[id].GetComponent<RectTransform>();
            _deleteItemRectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + _deleteItemRectYOffset);
            _slotIdToDeleteItem = id;
        }
        else {
            _slotIdToDeleteItem = -1;
            _deleteItemRectTransform.gameObject.SetActive(false);
        }
    }

    public void InventoryDeleteButtonPressed(int id) 
    {
        _slotIdToDeleteItem = -1;
        _deleteItemRectTransform.gameObject.SetActive(false);
        SendDeleteItem?.Invoke(id);
        UpdateInventory(_playerInventory.Inventory);
    }

    public void SaveGame()
    {
        GameSave.GameSaveController.Instance.SaveGame();
    }

    public void LoadGame()
    {
        GameFlowController.Instance.Restart();
    }

    public void RestartGame()
    {
        Bootstrap.LoadSaveOnStart = false;
        GameFlowController.Instance.Restart();
    }

    private void SwitchInventory() 
    {
        if (_inventory.activeInHierarchy)
        {
            _slotIdToDeleteItem = -1;
            _deleteItemRectTransform.gameObject.SetActive(false);
            _inventory.SetActive(false);
        }
        else {
            _inventory.SetActive(true);
            UpdateInventory(_playerInventory.Inventory);
        }
    }

    private void UpdateInventory(Inventory inventory)
    {
        if (_inventory.activeInHierarchy)
        {
            for (int i = 0; i < _inventoryButtons.Length; i++)
            {
                if (inventory.Items.Count <= i)
                {
                    _inventoryButtons[i].ClearSlot();
                }
                else
                {
                    _inventoryButtons[i].UpdateSlot(inventory.Items[i]);
                }
            }
        }
    }

    private void OnPlayerAmmoAmountChange(int ammo) 
    {
        _ammoText.text = ammo.ToString();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        GameEvents.PlayerAmmoAmountChanged -= OnPlayerAmmoAmountChange;
        GameEvents.PlayerInventoryUpdated -= UpdateInventory;
    }
}

