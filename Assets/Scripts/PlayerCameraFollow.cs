using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCameraFollow : MonoBehaviour
{
    private Vector3 _currentVelocity;
    private Rigidbody2D _playerRB;

    private Transform _target;
    [SerializeField] private Vector3 _followOffset;
    [SerializeField] private float _smoothTime;

    public void Init(GameObject player)
    {
        _playerRB = player.GetComponent<Rigidbody2D>();
        _target = player.transform;
    }

    public void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        currentPos = Vector3.SmoothDamp(currentPos, _target.position + _followOffset, ref _currentVelocity, _smoothTime);
        transform.position = currentPos;
    }
}
