using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: set initial volume values
    }

    public void SetMusicVolume(float vol)
    {
        audioMixer.SetFloat("Music", vol);
    }

    public void SetSFXVolume(float vol)
    {
        audioMixer.SetFloat("SFX", vol);
    }
}
