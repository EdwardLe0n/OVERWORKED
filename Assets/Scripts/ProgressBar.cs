using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxValue(float n)
    {
        slider.maxValue = n;
        slider.value = 0; // default to 0 bc no tasks will be done when level starts
    }

    public void IncrementValue()
    {
        slider.value += 1;
    }
}
