using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandlerPC : IPlayerInputHandler
{
    public Vector2 GetMovement()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public bool GetShooting()
    {
        return Input.GetButton("Fire1");
    }
}
