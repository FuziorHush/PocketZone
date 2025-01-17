using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Health _healthComponent;

    [SerializeField] private GameObject _barGameObject;
    [SerializeField] private Image _barFill;

    private void Awake()
    {
        _healthComponent.HealthChanged += OnHpChanged;
        _barGameObject.SetActive(false);
    }

    private void OnHpChanged(float health, float maxHealth)
    {
        if (health == maxHealth || health == 0)
            _barGameObject.SetActive(false);
        else {
            _barFill.fillAmount = 1 - (1 - (float)health / (float)maxHealth);
        }
    }
}
