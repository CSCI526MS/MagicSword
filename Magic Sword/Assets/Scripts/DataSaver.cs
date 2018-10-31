using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class DataSaver : MonoBehaviour
{
    private GameData gameData = new GameData();

    private readonly string gameDataFileName = "data.json";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnTrigger()
    {
        SavePlayerProgress();
        SaveGameData();
    }


    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
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
        gameData.itemList = inventory.itemList;
        InventorySlot [] inventorySlots = FindObjectsOfType<InventorySlot>();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            gameData.inventorySlots[i] = inventorySlots[i];
        }
        switch(SceneManager.GetActiveScene().name){
            case "LevelOne":
                gameData.level = 1;break;
            case "LevelTwo":
                gameData.level = 2;break;
            default:
                gameData.level = 1;break;
        }
    }
}