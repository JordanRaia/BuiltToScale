using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public AudioClip levelMusic;  // Assign this in the inspector for each level

    void Start()
    {
        MusicManager.Instance.SetAndPlayMusic(levelMusic);
    }
}
