using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager instance;

    public AudioSource audioSource;
    public AudioClip[] swipeSounds;
    public AudioClip resetSound;
    public AudioClip buttonClickSound;  // Add this to hold the button click sound

    private bool isAudioEnabled = true;

    void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Preserve this GameObject between scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }


    public void PlaySwipeSound()
    {
        if (isAudioEnabled && swipeSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, swipeSounds.Length);
            audioSource.clip = swipeSounds[randomIndex];
            audioSource.pitch = Random.Range(0.8f, 1.3f);
            audioSource.volume = 0.3f;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    public void PlayResetSound()
    {
        if (isAudioEnabled)
        {
            audioSource.clip = resetSound;
            audioSource.pitch = 1f;
            audioSource.volume = 0.5f;
            audioSource.time = 0.2f;
            audioSource.PlayScheduled(AudioSettings.dspTime);
        }
    }

    public void ToggleMute()
    {
        isAudioEnabled = !isAudioEnabled;
        audioSource.mute = !audioSource.mute;
    }

    public bool GetIsAudioEnabled() {
        return isAudioEnabled;
    }
}
