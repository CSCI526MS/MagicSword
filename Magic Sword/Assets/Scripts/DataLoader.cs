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
    public InventoryUI inventoryUI;
    public Inventory inventory;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainMenu");
#if UNITY_IOS
        root = Application.persistentDataPath;
#else
        root = Application.streamingAssetsPath;
#endif
        GlobalStatic.canvas.SetActive(false);
        //GlobalStatic.player.GetComponent<SpriteRenderer>().enabled = false;
        //GlobalStatic.inventoryUI.SetActive(false);
    }

    public bool LoadGameData(int mode)
    {
        string filePath = Path.Combine(root, newGameDataFileName);
        if (mode == 0)
        {
            filePath = Path.Combine(root, newGameDataFileName);
        }
        else if (mode == 1)
        {
            filePath = Path.Combine(root, gameDataFileName);
        }
        if (!File.Exists(filePath))
        {
            filePath = Path.Combine(root, newGameDataFileName);
            StreamWriter writer = new StreamWriter(filePath, true);
            string initData = "{\"inventorySlots\":[{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}}],\"equipmentSlots\":[{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}},{\"itemId\":\"\",\"properties\":[0,0,0,0],\"equipmentType\":0,\"icon\":{\"instanceID\":0},\"iconSelected\":{\"instanceID\":0}}],\"playerStatus\":{\"healthBar\":{\"instanceID\":66552},\"manaBar\":{\"instanceID\":66854},\"hpValue\":100.0,\"hpMaxValue\":100.0,\"speed\":6,\"attack\":10,\"defense\":0,\"mpValue\":100.0,\"mpMaxValue\":100.0},\"coordinate\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"level\":1,\"keyStatus\":[false,false,false]}";
            writer.WriteLine(initData);
            writer.Close();
        }
        string dataAsJson = File.ReadAllText(filePath);
        gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        return true;
    }

    public void LoadPlayerProgress()
    {
        GlobalStatic.background = GameObject.FindGameObjectWithTag("Background");
        //GlobalStatic.background.SetActive(false);
        //GlobalStatic.inventoryUI.SetActive(true);
        GlobalStatic.canvas.SetActive(true);
        //GlobalStatic.player.SetActive(true);
        GlobalStatic.inventoryUI.SetActive(true);
        Player player = FindObjectOfType<Player>();
        player.initPosition = gameData.coordinate;
        ManaBar manaBar = player.playerStatus.manaBar;
        HealthBar healthBar = player.playerStatus.healthBar;
        player.playerStatus = gameData.playerStatus;
        player.playerStatus.manaBar = manaBar;
        player.playerStatus.healthBar = healthBar;
        player.playerStatus.CurrentHP = gameData.playerStatus.hpValue;
        player.playerStatus.CurrentMP = gameData.playerStatus.mpValue;
        inventory = FindObjectOfType<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        InventorySlot[] inventorySlots = FindObjectsOfType<InventorySlot>();
        EquipmentSlot[] equipmentSlots = FindObjectsOfType<EquipmentSlot>();
        GlobalStatic.keyStatus = gameData.keyStatus;
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
                inventoryUI.inventory.inventorySlots[i].Item = new EquippableItem
                {
                    itemId = newItemData.itemId,
                    properties = newItemData.properties,
                    equipmentType = newItemData.equipmentType,
                    icon = newItemData.icon,
                    iconSelected = newItemData.iconSelected
                };
            }
            else {
                inventory.itemList[i] = null;
                inventoryUI.inventory.inventorySlots[i].Item = null;
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
            else
            {
                equipmentSlots[i].Item = null;
            }
        }
        switch (gameData.level)
        {
            case 1:
                GlobalStatic.crossSceneLevel = 1; break;
            case 2:
                GlobalStatic.crossSceneLevel = 2; break;
            case 3:
                GlobalStatic.crossSceneLevel = 3; break;
            default:
                GlobalStatic.crossSceneLevel = 1; break;
        }
    }

    public void SetUI(){
        GlobalStatic.inventoryUI.SetActive(false);
        GlobalStatic.player.GetComponent<SpriteRenderer>().enabled = true;
    }
}