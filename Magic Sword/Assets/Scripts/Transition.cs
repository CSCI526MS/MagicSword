using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {
	public void ChangeScene(string name) {
		SceneManager.LoadScene(name);
		Time.timeScale = 1f;
		GameObject.FindGameObjectWithTag("Player").SendMessage("DisablePanel");
	}
}
