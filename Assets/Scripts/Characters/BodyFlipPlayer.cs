using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFlipPlayer : MonoBehaviour
{
    [SerializeField] private Transform _bodyTransform;
    private bool _flipped;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rigidbody.velocity.x < 0)
        {
            if (!_flipped)
            {
                _bodyTransform.localScale = new Vector3(-_bodyTransform.localScale.x, _bodyTransform.localScale.y, _bodyTransform.localScale.z);
                _flipped = true;
            }
        }
        else
        {
            if (_flipped)
            {
                _bodyTransform.localScale = new Vector3(-_bodyTransform.localScale.x, _bodyTransform.localScale.y, _bodyTransform.localScale.z);
                _flipped = false;
            }
        }
    }
}
