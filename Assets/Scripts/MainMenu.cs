using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Select Level");
        //MusicManager.Instance.StopMusic();
        // SceneManager.LoadScene("Level1");1
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
