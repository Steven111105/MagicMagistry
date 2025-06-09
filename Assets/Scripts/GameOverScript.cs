using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text enemiesKilledText;
    public void ShowResult()
    {
        Debug.Log("Game Over! Time: " + Time.timeSinceLevelLoad + " seconds, Enemies Killed: " + PlayerStats.enemiesKilled);
        gameObject.SetActive(true);
        timeText.text = "Time Survived:\n" + Time.timeSinceLevelLoad.ToString("F2") + " seconds";
        enemiesKilledText.text = "Enemies Killed:\n" + PlayerStats.enemiesKilled.ToString();
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        // Quit the application
        SceneManager.LoadScene("MainMenu");
    }
}
