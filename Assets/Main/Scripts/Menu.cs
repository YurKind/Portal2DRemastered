using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void NewGame()
	{
		SceneManager.LoadScene("Level1");
	}
	public void Continue()
	{
		var levelToLoad = File.ReadAllText("save.txt");
		SceneManager.LoadScene(levelToLoad);
	}
	public void Exit()
	{
		Application.Quit();
	}
}
