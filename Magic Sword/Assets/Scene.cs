using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour {
	void Start () {
        int level;
        switch (SceneManager.GetActiveScene().name)
        {
            case "LevelOne": level = 0; break;
            case "LevelTwo": level = 1; break;
            case "LevelThree": level = 2; break;
            default: level = 0; break;
        }
        DataLoader dataLoader = FindObjectOfType<DataLoader>();
        if (dataLoader.gameData.keyStatus[level])
        {
            Destroy(GameObject.FindWithTag("Gate"));
        }
    }
}
