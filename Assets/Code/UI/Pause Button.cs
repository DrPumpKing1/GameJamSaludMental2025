using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private static bool isPaused = false;
    public GameObject pauseMenu;

    private void Start()
    {
       
    }

    public void Pause()
    {
   
        isPaused = true;

        pauseMenu?.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }


    public bool IsPaused() => isPaused;

    public void QuitGame()
    {
        Application.Quit();
    }
}


