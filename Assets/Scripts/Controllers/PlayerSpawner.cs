using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSave;

public class PlayerSpawner : MonoSingleton<PlayerSpawner>
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _cameraPrefab;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SpawnPlayer()
    {
        GameObject player = CreatePlayer(SceneObjects.Instance.PlayerSpawnPoint.position);
        CreateCamera(player);
    }

    public PlayerSaveData GetPlayerData() 
    {
        GameObject player = SceneObjects.Instance.PlayerLink;
        CharacterSaveData characterData = new CharacterSaveData(new PositionSave(player.transform.position), player.GetComponent<Health>().CurrentHealth);

        Inventory inventory = player.GetComponent<PlayerInventory>().Inventory;
        ItemSaveData[] itemSaveDatas = new ItemSaveData[inventory.Items.Count];
        for (int i = 0; i < inventory.Items.Count; i++)
        {
             itemSaveDatas[i] = new ItemSaveData(inventory.Items[i].Item.ID, inventory.Items[i].Amount);
        }

        return new PlayerSaveData(characterData, player.GetComponent<PlayerShooting>().Ammo, itemSaveDatas);
    }

    public void LoadPlayer(PlayerSaveData playerData)
    {
        Vector3 position = new Vector3(playerData.CharacterData.Position.X, playerData.CharacterData.Position.Y, playerData.CharacterData.Position.Z);
        GameObject player = CreatePlayer(position);
        player.GetComponent<Health>().SetHealth(playerData.CharacterData.Health);
        player.GetComponent<PlayerShooting>().SetAmmo(playerData.PlayerAmmo);

        Inventory inventory = player.GetComponent<PlayerInventory>().Inventory;
        for (int i = 0; i < playerData.PlayerItems.Length; i++)
        {
            ItemConfig itemConfig = ItemsDatabase.Instance.GetItemConfigByID(playerData.PlayerItems[i].ID);
            Item item = new Item(itemConfig.ID, itemConfig.Name, itemConfig.Stacks, itemConfig.PutsInSlot, itemConfig.Icon);
            inventory.PutItem(item, playerData.PlayerItems[i].Amount);
        }

        CreateCamera(player);
    }

    private GameObject CreatePlayer(Vector3 position) 
    {
        GameObject player = Instantiate(_playerPrefab);
        player.transform.position = position;
        player.transform.SetParent(SceneObjects.Instance.CharactersParent);
        player.GetComponent<PlayerBase>().Init();
        SceneObjects.Instance.PlayerLink = player;

        return player;
    }

    private void CreateCamera(GameObject player)
    {
        GameObject camera = Instantiate(_cameraPrefab, player.transform.position, player.transform.rotation);
        camera.GetComponent<PlayerCameraFollow>().Init(player);
    }
}