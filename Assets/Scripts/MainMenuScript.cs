using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
