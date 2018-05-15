using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void NewGame()
	{
		SceneManager.LoadScene("Level1");
	}
	public void Continue()
	{
		
	}
	public void Levels()
	{
		
	}
	public void Exit()
	{
		Application.Quit();
	}
}
