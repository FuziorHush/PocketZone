using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSave
{
    public class GameSaveController : MonoSingleton<GameSaveController>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void SaveGame()
        {
            GameSaveData gameSave = new GameSaveData(PlayerSpawner.Instance.GetPlayerData(), EnemiesSpawner.Instance.GetEnemiesData());
            SaveLoadManager.SaveGame(gameSave);
        }

        public GameSaveData LoadSave()
        {
            return SaveLoadManager.LoadGame();
        }
    }

    [System.Serializable]
    public class GameSaveData
    {
        public GameSaveData(PlayerSaveData playerSaveData, EnemySaveData[] enemyData) {
            PlayerData = playerSaveData;
            EnemyData = enemyData;
        }

        public PlayerSaveData PlayerData;
        public EnemySaveData[] EnemyData;
    }

    [System.Serializable]
    public class PlayerSaveData
    {
        public PlayerSaveData(CharacterSaveData characterData, int playerAmmo, ItemSaveData[] playerItems)
        {
            CharacterData = characterData;
            PlayerAmmo = playerAmmo;
            PlayerItems = playerItems;
        }

        public CharacterSaveData CharacterData;
        public int PlayerAmmo;
        public ItemSaveData[] PlayerItems;
    }

    [System.Serializable]
    public class EnemySaveData
    {
        public EnemySaveData(CharacterSaveData characterData, int id)
        {
            CharacterData = characterData;
            ID = id;
        }

        public CharacterSaveData CharacterData;
        public int ID;
    }

    [System.Serializable]
    public class CharacterSaveData
    {
        public CharacterSaveData(Vector3 position, float health)
        {
            Position = position;
            Health = health;
        }

        public Vector3 Position;
        public float Health;
    }

    [System.Serializable]
    public class ItemSaveData
    {
        public ItemSaveData(int id, int amount)
        {
            ID = id;
            Amount = amount;
        }

        public int ID;
        public int Amount;
    }
}