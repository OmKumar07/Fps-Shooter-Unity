using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public bool grAdient = true;
    public Gradient gradient;
    public Image fill;
    public bool ExtraRedBorder = true;
    public GameObject RedBorder;

    public void SetMaxHealth (float health)
    {
        slider.maxValue = health;
        slider.value = health;
        if(grAdient == true)
        {
            fill.color = gradient.Evaluate(1f);
        }
        
    }
    public void Sethealth(float health)
    {
        slider.value = health;
        if(grAdient == true)
        {
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
        
    }

    void Update()
    {
        if(ExtraRedBorder == true)
        {
            if (slider.value < 200)
            {
                RedBorder.SetActive(true);

            }
            else
                RedBorder.SetActive(false);
        }
        
    }
}
