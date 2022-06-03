using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float health)
    {
        float max_value = slider.maxValue;
        slider.maxValue = health;
        slider.value += slider.maxValue - max_value;
        
    }

    public void SetHealth(float health)
    {
        slider.value = health;

    }
}
