using Assets._3D4amb_LIB;
using Assets._3D4amb_LIB.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this to the slider for the healthy eye
/// </summary>
public class HealthyEyeSlider : SliderScript
{
    protected override void SetTag()
    {
        SliderTag = "Healthy eye: ";
    }

    protected override void InitSliderValue()
    {
        PlayerSettings ps = PrefManager.GetComponent<PrefManager>().LoadPlayerSettings();
        gameObject.GetComponent<Slider>().value = (int)ps.healthyEye;
    }

    public override void UpdateSlider()
    {
        base.UpdateSlider();
        string val = ((Eye)(int)v).ToString();
        base.SetText(val);
    }
}
