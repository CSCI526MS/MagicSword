using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class DataSaver : MonoBehaviour
{
    public GameData gameData = new GameData();

    private readonly string gameDataFileName = "data.json";
    private string root;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
#if UNITY_IOS
        root = Application.persistentAssetsPath;
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
        Debug.Log(inventory);
        for (int i = 0; i < inventory.itemList.Length; i++)
        {
            if(inventory.itemList[i] != null){
                gameData.itemList[i].itemId = inventory.itemList[i].itemId;
                gameData.itemList[i].properties = inventory.itemList[i].properties;
                gameData.itemList[i].icon = inventory.itemList[i].icon;
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