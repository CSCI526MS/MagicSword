using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("select");
        LoadDataFromLocal(0);
        SceneManager.LoadScene("LevelOne");
    }

    public void ContinueGame() {
        FindObjectOfType<AudioManager>().Play("select");
        LoadDataFromLocal(1);
        switch (GlobalStatic.crossSceneLevel){
            case 1: SceneManager.LoadScene("LevelOne");break;
            case 2: SceneManager.LoadScene("LevelTwo"); break;
            default: SceneManager.LoadScene("LevelOne"); break;
        }
        
    }

	public void QuitGame() {
        FindObjectOfType<AudioManager>().Play("select");
		Debug.Log("QUIT!");
		Application.Quit();
	}

    private void LoadDataFromLocal(int mode){
        DataLoader dataLoader = FindObjectOfType<DataLoader>();
        if (dataLoader.LoadGameData(mode))
        {
            dataLoader.LoadPlayerProgress();
            dataLoader.SetUI();
        }
        else
        {
            dataLoader.SetUI();
            SceneManager.LoadScene("LevelOne");
        }
    }
}
