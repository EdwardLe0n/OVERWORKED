using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        // load saved volume settings
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.8f);

        // Apply the loaded settings
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float vol)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(vol) * 20); // fancy math to convert to decibels for audio mixer
        PlayerPrefs.SetFloat("MusicVolume", vol);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float vol)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(vol) * 20); // ditto
        PlayerPrefs.SetFloat("SFXVolume", vol);
        PlayerPrefs.Save();
    }
}
