using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	public void PlayGame() {
		SceneManager.LoadScene("MainMenu");
        GlobalStatic.crossSceneInfo = 0;
	}

	public void ContinueGame() {
		SceneManager.LoadScene("MainMenu");
        GlobalStatic.crossSceneInfo = 1;
    }

	public void QuitGame() {
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
