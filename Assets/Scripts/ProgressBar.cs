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
        slider.value = 0; // set to 0 bc no tasks completed at beginning of level
    }

    public void IncrementValue()
    {
        slider.value += 1;
    }

    public void CheckValue()
    {
        Debug.Log("value= " + slider.value);
    }
}
