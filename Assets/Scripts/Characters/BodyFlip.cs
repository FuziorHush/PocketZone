using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFlip : MonoBehaviour
{
    [SerializeField] private Transform _bodyTransform;
    private bool _flipped;
    private Vector3 _previousPosition;

    private void Awake()
    {
        _previousPosition = transform.position;
    }

    private void Update()
    {
        Vector3 vec = transform.position - _previousPosition;
        if (vec.x < 0)
        {
            if (!_flipped)
            {
                _bodyTransform.localScale = new Vector3(-_bodyTransform.localScale.x, _bodyTransform.localScale.y, _bodyTransform.localScale.z);
                _flipped = true;
            }
        }
        else {
            if (_flipped)
            {
                _bodyTransform.localScale = new Vector3(-_bodyTransform.localScale.x, _bodyTransform.localScale.y, _bodyTransform.localScale.z);
                _flipped = false;
            }
        }
        _previousPosition = transform.position;
    }
}
