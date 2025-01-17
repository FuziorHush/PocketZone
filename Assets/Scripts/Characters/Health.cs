using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _health;
    private float _maxHealth;

    public UnityAction<float, float> HpChanged;

    public void Init(float health) {
        _maxHealth = health;
        _health = health;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health < 0)
            _health = 0;

        HpChanged?.Invoke(_health, _maxHealth);
    }
}
