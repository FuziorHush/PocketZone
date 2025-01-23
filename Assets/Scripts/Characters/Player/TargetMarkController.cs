using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetMarkController : IPlayerContextUpdate
{
    private GameObject _mark;
    private Transform _markTransform;
    private SpriteRenderer _markSpriteRenderer;

    private PlayerEnemiesDetection _playerEnemiesDetection;

    private Transform _currentTarget;

    public void Init(PlayerEnemiesDetection playerEnemiesDetection, GameObject targetMarkPrefab) 
    {
        _playerEnemiesDetection = playerEnemiesDetection;

        _mark = GameObject.Instantiate(targetMarkPrefab);
        _markSpriteRenderer = _mark.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _markSpriteRenderer.color = Color.clear;
        _markTransform = _mark.transform;
    }

    public void OnUpdate()
    {
        if (_playerEnemiesDetection.TargetEnemy == null)
        {
            if (_mark.activeInHierarchy)
            {
                _currentTarget = null;
                _mark.SetActive(false);
            }
        }
        else if (_currentTarget != _playerEnemiesDetection.TargetEnemy)
        {
            _markTransform.position = _playerEnemiesDetection.TargetEnemy.position;
            _currentTarget = _playerEnemiesDetection.TargetEnemy;
            PlaySetMarkAnimation();
        }
        else
        {
            _markTransform.position = _playerEnemiesDetection.TargetEnemy.position;
        }
    }

    private void PlaySetMarkAnimation() 
    {
        _mark.SetActive(true);
        _markSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        _markTransform.localScale = new Vector3(2, 2, 2);
        _markSpriteRenderer.DOFade(1f, 0.5f);
        _markTransform.DOScale(1f, 0.5f);
    }
}
