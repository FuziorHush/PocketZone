using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandlerMobile : IPlayerInputHandler
{
    private PlayerInventory _playerInventory;
    private HUD _hud;

    public void Init(GameObject player) {
        _playerInventory = player.GetComponent<PlayerBase>().PlayerInventory;
        _hud = HUD.Instance;
        _hud.SendDeleteItem += DeleteItem;
    }

    public void DeleteItem(int id)
    {
        _playerInventory.DeleteItem(id);
    }

    public Vector2 GetMovement()
    {
        return _hud.GetJoystickAxis();
    }

    public bool GetShooting()
    {
        return _hud.GetShootButton();
    }
}
