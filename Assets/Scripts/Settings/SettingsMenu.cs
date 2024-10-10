using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
