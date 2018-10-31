using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	public void PlayGame() {
		SceneManager.LoadScene("LevelOne");
	}

	public void ContinueGame() {
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame() {
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
