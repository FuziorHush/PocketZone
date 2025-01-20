using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoSingleton<GameFlowController>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void Init() 
    {
        GameEvents.PlayerDied += Restart;
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        GameEvents.PlayerDied -= Restart;
    }
}
