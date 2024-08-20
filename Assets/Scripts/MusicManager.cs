using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;
    private AudioSource audioSource;

    public static MusicManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void SetAndPlayMusic(AudioClip newClip)
    {
        // Check if a different clip needs to be played or if nothing is currently playing
        if (audioSource.clip != newClip || !audioSource.isPlaying)
        {
            audioSource.clip = newClip;  // Assign the new clip
            audioSource.Play();          // Start playing the new clip
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void ToggleMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }
}
