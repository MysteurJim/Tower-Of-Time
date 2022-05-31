using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnnemie : MonoBehaviour
{
    public Slider sliderEnnemie;

    public void SetMaxHealthEnnemie(float health)
    {
        sliderEnnemie.maxValue = health;
        sliderEnnemie.value = health;
    }

    public void SetHealthEnnemie(float health)
    {
        sliderEnnemie.value = health;
    }
}
