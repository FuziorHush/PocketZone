using UnityEngine;

public interface IPlayerInputHandler
{
    void Init(GameObject player);

    Vector2 GetMovement();
    bool GetShooting();
    void DeleteItem(int id);
}
