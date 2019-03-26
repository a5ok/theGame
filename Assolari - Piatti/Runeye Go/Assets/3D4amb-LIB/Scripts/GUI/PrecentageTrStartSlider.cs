using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrecentageTrStartSlider : PercentageSlider
{
    protected override void InitSliderValue()
    {
        PenaltyInfo PI = PrefManager.GetComponent<PrefManager>().LoadPlayerPIstart();
        gameObject.GetComponent<Slider>().value = PI.PenaltyTransparency;
    }
}
