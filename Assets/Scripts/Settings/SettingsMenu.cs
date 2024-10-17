using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject controlsWindow;
    
    public void ToggleSelf()
    {
        gameObject.SetActive(false);
    }

    public void ToggleControlsWindow()
    {
        controlsWindow.SetActive(!controlsWindow.activeSelf);
    }

    // for the arachnophobia mode toggle (which doesn't do anything)
    public Toggle toggle;

    void Start()
    {
        int state = PlayerPrefs.GetInt("toggle", 0); // get saved toggle state
        if(state == 1) {
            toggle.isOn = true;
        }
        else {
            toggle.isOn = false;
        }

        toggle.onValueChanged.AddListener(ArachnophobiaToggle);
    }

    // save toggle state
    public void ArachnophobiaToggle(bool isOn)
    {
        bool b = isOn;
        if(b) {
            PlayerPrefs.SetInt("toggle", 1);
        }
        else {
            PlayerPrefs.SetInt("toggle", 0);
        }
        PlayerPrefs.Save();
    }
}
