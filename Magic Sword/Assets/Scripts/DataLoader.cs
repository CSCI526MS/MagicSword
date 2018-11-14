using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class DataLoader : MonoBehaviour
{
    public GameData gameData;

    private readonly string gameDataFileName = "data.json";
    private readonly string newGameDataFileName = "data.default.json";
    private string root;
    private static GameObject inventoryUI;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainMenu");
#if UNITY_IOS
        root = Application.persistentDataPath;
#else
        root = Application.streamingAssetsPath;
#endif
    }

    public bool LoadGameData(int mode)
    {
        string filePath = Path.Combine(root, newGameDataFileName);
        if (mode == 0) {
            filePath = Path.Combine(root, newGameDataFileName);
        }
        else if(mode == 1) {
            filePath = Path.Combine(root, gameDataFileName);
        }
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
            return true;
        }
        else
        {
            Debug.Log("Cannot load game data! Starting new game.");
            return false;
        }
    }

    public void LoadPlayerProgress()
    {
        Player player = FindObjectOfType<Player>();
        player.initPosition = gameData.coordinate;
        ManaBar manaBar = player.playerStatus.manaBar;
        HealthBar healthBar = player.playerStatus.healthBar;
        player.playerStatus = gameData.playerStatus;
        player.playerStatus.manaBar = manaBar;
        player.playerStatus.healthBar = healthBar;
        Inventory inventory = FindObjectOfType<Inventory>();
        InventorySlot[] inventorySlots = FindObjectsOfType<InventorySlot>();
        EquipmentSlot[] equipmentSlots = FindObjectsOfType<EquipmentSlot>();
        for (int i = 0; i < GlobalStatic.inventorySlotNum; i++)
        {
            if (!gameData.inventorySlots[i].itemId.Equals("")){
                ItemData newItemData = gameData.inventorySlots[i];
                inventory.itemList[i] = new EquippableItem
                {
                    itemId = newItemData.itemId,
                    properties = newItemData.properties,
                    equipmentType = newItemData.equipmentType,
                    icon = newItemData.icon,
                    iconSelected = newItemData.iconSelected
                };
                inventorySlots[i].Item = new EquippableItem
                {
                    itemId = newItemData.itemId,
                    properties = newItemData.properties,
                    equipmentType = newItemData.equipmentType,
                    icon = newItemData.icon,
                    iconSelected = newItemData.iconSelected
                };
            }
        }
        for (int i = 0; i < GlobalStatic.equipmentSlotNum; i++)
        {
            if (gameData.equipmentSlots[i].itemId != null)
            {
                ItemData newItemData = gameData.equipmentSlots[i];
                if (!gameData.equipmentSlots[i].itemId.Equals(""))
                {
                    equipmentSlots[i].Item = new EquippableItem
                    {
                        itemId = newItemData.itemId,
                        properties = newItemData.properties,
                        equipmentType = newItemData.equipmentType,
                        icon = newItemData.icon,
                        iconSelected = newItemData.iconSelected
                    };
                }
            }
        }
        switch (gameData.level)
        {
            case 1:
                GlobalStatic.crossSceneLevel = 1; break;
            case 2:
                GlobalStatic.crossSceneLevel = 2; break;
            default:
                GlobalStatic.crossSceneLevel = 1; break;
        }
        if (inventoryUI == null)
        {
            inventoryUI = GameObject.FindWithTag("InventoryUI");
        }
        inventoryUI.SetActive(false);
    }
}