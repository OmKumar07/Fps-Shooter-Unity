using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadUi : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxBullet(float bullet)
    {
        slider.maxValue = bullet;
        slider.value = bullet;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetBullet(float bullet)
    {
        slider.value = bullet;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
