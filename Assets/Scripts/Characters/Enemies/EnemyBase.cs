using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private IEnemyState[] _states = new IEnemyState[2];
    private IEnemyState _currentState;
    //[SerializeField] private  _config;

    private void Awake()
    {
        _states[0] = GetComponent<EnemyStateIdle>();
        _states[1] = GetComponent<EnemyStateAttack>();
    }

    public void Init() 
    {
        _currentState = _states[0];
        _currentState.OnActivate();
    }
}
