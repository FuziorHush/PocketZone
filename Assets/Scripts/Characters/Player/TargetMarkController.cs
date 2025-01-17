using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarkController : MonoBehaviour
{
    [SerializeField] private GameObject _mark;
    private Transform _markTransform;
    private SpriteRenderer _markSpriteRenderer;

    private PlayerEnemiesDetection _playerDetection;

    private Transform _currentTarget;

    public void Init() 
    {
        _markTransform = _mark.transform;
        _markSpriteRenderer = _mark.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_playerDetection.TargetEnemy == null)
        {
            if (_mark.activeInHierarchy)
            {
                _mark.SetActive(false);
            }
        }
        else if (_currentTarget != _playerDetection.TargetEnemy)
        {
            StopCoroutine("MarkSetAnimatiion");
            StartCoroutine("MarkSetAnimatiion");
            //install dotween
        }
        else 
        {
            _markTransform.position = _playerDetection.TargetEnemy.position;
        }
    }

    private IEnumerator MarkSetAnimatiion() {
        _markSpriteRenderer.color = Color.clear;
        _markSpriteRenderer.transform.localScale = new Vector3(2, 2, 2);
        float colorAlpha = 0;
        float scale = 2;
        for (int i = 0; i < 20; i++)
        {
            yield return null;
            colorAlpha += 0.05f;
            scale -= 0.05f;
            //_markSpriteRenderer. = new Color()
        }
        //install dotween lol
    }
}
