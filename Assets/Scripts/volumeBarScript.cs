using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void increaseVolume()
    {
        if (slider.value != slider.maxValue)
        {
            slider.value++;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            AudioListener.volume = ((float)(slider.value)) / 100f;
        }
    }
    public void decreaseVolume()
    {
        if (slider.value > 0)
        {
            slider.value--;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            AudioListener.volume = ((float)(slider.value)) / 100f; 
        }
    }

    

    public void setMaxVolume(int _x)
    {
        slider.maxValue = _x;
        slider.value = _x;
        fill.color = gradient.Evaluate(1f);
    }
}
