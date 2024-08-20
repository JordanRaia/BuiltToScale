using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelect : MonoBehaviour
{
    //public Button[] buttons;
    //public void OpenLevel(int levelID){
    //    string levelName = "Level" + levelID;
    //    SceneManager.LoadScene(levelName);
    //}
    //private void Awake(){
    //    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel",1);
    //    for (int i = 0; i < buttons.Length; i++){
    //        buttons[i].interactable = false;
    //    }

    //    for (int i = 0; i < unlockedLevel; i++){
    //        buttons[i].interactable = true;
    //    }
    //}

    public void Level1()
    {
        SceneManager.LoadSceneAsync(3);
        // SceneManager.LoadScene("Select Level", LoadSceneMode.Additive);
    }
    public void Level2()
    {
        SceneManager.LoadSceneAsync(4);
        // SceneManager.LoadScene("Select Level", LoadSceneMode.Additive);
    }
    public void Level3()
    {
        SceneManager.LoadSceneAsync(5);
        // SceneManager.LoadScene("Select Level", LoadSceneMode.Additive);
    }
    public void Level4()
    {
        SceneManager.LoadSceneAsync(6);
        // SceneManager.LoadScene("Select Level", LoadSceneMode.Additive);
    }
    public void Level5()
    {
        SceneManager.LoadSceneAsync(7);
        // SceneManager.LoadScene("Select Level", LoadSceneMode.Additive);
    }
}
