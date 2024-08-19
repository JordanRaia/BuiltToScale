using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelect : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadSceneAsync(3);
        // SceneManager.LoadScene("Select Level", LoadSceneMode.Additive);
    }
}
