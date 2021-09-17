using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene("Level01");
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
	
}
