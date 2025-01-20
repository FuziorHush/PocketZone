using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandlerPC : IPlayerInputHandler
{
    private PlayerInventory _playerInventory;

    public void Init(GameObject player)
    {
        _playerInventory = player.GetComponent<PlayerInventory>();
    }

    public void DeleteItem(int id)
    {
        _playerInventory.DeleteItem(id);
    }

    public Vector2 GetMovement()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public bool GetShooting()
    {
        return Input.GetButton("Fire1");
    }
}
