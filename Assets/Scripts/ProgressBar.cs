using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
<<<<<<< Updated upstream
    public Image fill;
    
    public void SetMaxValue(float n)
    {
        slider.maxValue = n;
        slider.value = 0; // bc max value is set at beginning of level when no tasks are completed
    }

    // call when completing tasks
=======

    public void SetMaxValue(float n)
    {
        slider.maxValue = n;
        slider.value = 0; // default to 0 bc no tasks will be done when level starts
    }

>>>>>>> Stashed changes
    public void IncrementValue()
    {
        slider.value += 1;
    }
}
