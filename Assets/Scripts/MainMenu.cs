using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
        // SceneManager.LoadScene("Select Level", LoadSceneMode.Additive);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
