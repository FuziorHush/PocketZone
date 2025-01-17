using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveController : MonoBehaviour
{
    public static GameSave GameSave;

    private void Awake()
    {

    }

    public void Init()
    {
        if (!SaveLoadManager.CheckSaveFileExists())
        {
            CreateGameSave();
            SaveGame();
        }
        else
        {
            LoadSave();
        }
    }

    public void CreateGameSave()
    {
        GameSave = new GameSave();

    }

    public void SaveGame()
    {
        SaveLoadManager.SaveGame(GameSave);
    }

    public void LoadSave()
    {
        GameSave = SaveLoadManager.LoadGame();
    }

    public void ResetGameSave() 
    {
        CreateGameSave();
        SaveGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

[System.Serializable]
public class GameSave
{

}
