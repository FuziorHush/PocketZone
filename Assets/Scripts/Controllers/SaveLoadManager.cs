using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager
{
    private static string GameSavePath;

    static SaveLoadManager()
    {
        GameSavePath = Application.persistentDataPath + "/gamesave.save";
    }

    public static bool CheckSaveFileExists()
    {
       return File.Exists(GameSavePath);
    }

    public static bool SaveGame(GameSave gameSave)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(GameSavePath, FileMode.Create);

        try
        {
            binaryFormatter.Serialize(fileStream, gameSave);
            fileStream.Close();
            return true;
        }
        catch (System.Exception)
        {
            fileStream.Close();
            return false;
        }
    }

    public static GameSave LoadGame()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(GameSavePath, FileMode.Open);

        GameSave gameSave;
        try
        {
            gameSave = (GameSave)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return gameSave;
        }
        catch (System.Exception)
        {
            fileStream.Close();
            return null;
        }
    }
}