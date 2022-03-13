using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healhBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void updateHealth(int health)
    {
        slider.value += health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setMaxHealth(int _x)
    {
        slider.maxValue = _x;
        slider.value = _x;
        fill.color = gradient.Evaluate(1f);
    }


}