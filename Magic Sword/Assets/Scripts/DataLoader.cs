using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class DataLoader : MonoBehaviour
{
    private GameData gameData;

    private string gameDataFileName = "data.json";
    private string newGameDataFileName = "data.default.json";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public bool LoadGameData(int mode)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, newGameDataFileName);
        if (mode == 0) {
            filePath = Path.Combine(Application.streamingAssetsPath, newGameDataFileName);
        }
        else if(mode == 1) {
            filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
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
        inventory.itemList = gameData.itemList;
        InventorySlot [] inventorySlots = FindObjectsOfType<InventorySlot>();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = gameData.inventorySlots[i];
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
    }
}