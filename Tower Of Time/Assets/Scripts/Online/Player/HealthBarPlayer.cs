using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    public Slider sliderPlayer;

    public void SetMaxHealthPlayer(float health)
    {
        sliderPlayer.maxValue = health;
        sliderPlayer.value = health;
    }

    public void SetHealthPlayer(float health)
    {
        sliderPlayer.value = health;
    }
}
