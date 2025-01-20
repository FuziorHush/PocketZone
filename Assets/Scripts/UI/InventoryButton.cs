using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _textAmount;

    private int _id;
    private HUD _hud;
    private bool _hasItem;

    public void Init(HUD hud, int id)
    {
        _hud = hud;
        _id = id;
        GetComponent<Button>().onClick.AddListener(ButtonPressed);
    }

    public void UpdateSlot(InventoryItem inventoryItem) 
    {
        _itemImage.color = Color.white;
        _itemImage.sprite = inventoryItem.Item.Icon;
        if (inventoryItem.Amount > 1)
        {
            _textAmount.text = inventoryItem.Amount.ToString();
        }
        else {
            _textAmount.text = string.Empty;
        }
        _hasItem = true;
    }

    public void ClearSlot()
    {
        _itemImage.color = Color.clear;
        _textAmount.text = string.Empty;
        _hasItem = false;
    }

    private void ButtonPressed() 
    {
        if(_hasItem)
        _hud.InventoryClotButtonPressed(_id);
    }
}
