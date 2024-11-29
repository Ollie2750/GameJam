using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffects : MonoBehaviour
{

    [Header("Audio Source")]
    AudioSource SFXSource;

    private static soundEffects instance;

    // Makes the gameobject persist between scenes
    private void Awake()
    {
        // Ensure there's only one instance of SFXManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make the GameObject persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }


    
}
