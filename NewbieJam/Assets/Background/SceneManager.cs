using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGameScene()
    {
        scoreKeeper.ResetScore();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        scoreKeeper.ResetScore();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
