using UnityEngine;
using UnityEngine.SceneManagement;

// Small behaviour to handle menu button callbacks.
public class MenuController : MonoBehaviour
{
	// When the start or retry button is pressed, load the Game scene.
	public void OnStartClicked()
	{
		SceneManager.LoadScene("Game");
	}

	// When the menu button is clicked, load the Menu scene.
	public void OnBackClicked()
	{
		SceneManager.LoadScene("Menu");
	}

	// When the quit button is clicked, quit the application.
 	public void OnQuitClicked()
	{
		Application.Quit();
	}
}
