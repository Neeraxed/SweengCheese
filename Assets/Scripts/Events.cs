using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
	public void QuitGame()
	{
		Application.Quit();
	}
	public void LoadScene(int index)
	{
		SceneManager.LoadScene(index);
	}
}
