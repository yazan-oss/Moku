using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            {
            if (GameIsPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }
    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void loadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
