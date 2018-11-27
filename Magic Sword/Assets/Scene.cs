using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour {
	void Start () {
        int level;
        string gateName;
        switch (SceneManager.GetActiveScene().name)
        {
            case "LevelOne": level = 0; gateName = "Gate_1"; break;
            case "LevelTwo": level = 1; gateName = "Gate_2"; break;
            case "LevelThree": level = 2; gateName = "Gate_3"; break;
            default: level = 0; gateName = ""; break;
        }
        DataLoader dataLoader = FindObjectOfType<DataLoader>();
        if (dataLoader.gameData.keyStatus[level])
        {
            GameObject gate = GameObject.Find(gateName);
            gate.GetComponent<SpriteRenderer>().enabled = false;
            gate.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
