using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class DataSaver : MonoBehaviour
{
    public GameData gameData = new GameData();
    private GameObject inventoryUI;

    private readonly string gameDataFileName = "data.json";
    private string root;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
#if UNITY_IOS
        root = Application.persistentDataPath;
#else
        root = Application.streamingAssetsPath;
#endif
    }

    public void OnTrigger()
    {
        SavePlayerProgress();
        SaveGameData();
    }


    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Path.Combine(root, gameDataFileName);
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        File.WriteAllText(filePath, dataAsJson);
    }

    private void SavePlayerProgress()
    {
        Player player = FindObjectOfType<Player>();
        gameData.coordinate = player.transform.position;
        gameData.playerStatus = player.playerStatus;
        Inventory inventory = FindObjectOfType<Inventory>();
        InventorySlot[] inventorySlots = FindObjectOfType<InventoryUI>().inventory.inventorySlots;
        EquipmentSlot[] equipmentSlots = FindObjectsOfType<EquipmentSlot>();
        gameData.keyStatus = GlobalStatic.keyStatus;
        for (int i = 0; i < GlobalStatic.inventorySlotNum; i++)
        {
            Debug.Log(i);
            if (inventorySlots[i].item != null)
            {
                gameData.inventorySlots[i].itemId = inventorySlots[i].item.itemId;
                gameData.inventorySlots[i].properties = inventorySlots[i].item.properties;
                gameData.inventorySlots[i].equipmentType = inventorySlots[i].item.equipmentType;
                gameData.inventorySlots[i].icon = inventorySlots[i].item.icon;
                gameData.inventorySlots[i].iconSelected = inventorySlots[i].item.iconSelected;
            }
            else{
                gameData.inventorySlots[i] = new ItemData();
            }
        }
        for (int i = 0; i < GlobalStatic.equipmentSlotNum; i++)
        {
            Debug.Log(equipmentSlots[i].item);
            if (equipmentSlots[i].item != null)
            {
                gameData.equipmentSlots[i].itemId = equipmentSlots[i].item.itemId;
                gameData.equipmentSlots[i].properties = equipmentSlots[i].item.properties;
                gameData.equipmentSlots[i].equipmentType = equipmentSlots[i].item.equipmentType;
                gameData.equipmentSlots[i].icon = equipmentSlots[i].item.icon;
                gameData.equipmentSlots[i].iconSelected = equipmentSlots[i].item.iconSelected;
            }
            else
            {
                gameData.equipmentSlots[i] = new ItemData();
            }
        }
        switch (SceneManager.GetActiveScene().name){
            case "LevelOne":
                gameData.level = 1;break;
            case "LevelTwo":
                gameData.level = 2;break;
            default:
                gameData.level = 1;break;
        }
    }
}