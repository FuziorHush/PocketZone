using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _maxHealth;

    private float _health;
    private float _damageTakenThisFrame;

    public UnityAction<float, float> HealthChanged;

    public void Init(float health) {
        _maxHealth = health;
        _health = health;
    }

    private void Update()
    {
        if (_damageTakenThisFrame > 0) {
            _health -= _damageTakenThisFrame;
            _damageTakenThisFrame = 0;
            if (_health < 0)
                _health = 0;

            HealthChanged?.Invoke(_health, _maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        _damageTakenThisFrame += damage;
    }
}
