using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void OnActivate();
    void OnDeactivate();

    void OnUpdate();
}
