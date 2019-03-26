using Assets._3D4amb_LIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

class PercentageEyePStart : PercentageSlider
{
    protected override void InitSliderValue()
    {
        PenaltyInfo PI = PrefManager.GetComponent<PrefManager>().LoadPlayerPIstart();
        gameObject.GetComponent<Slider>().value = PI.PenaltyEyePatch;
    }
}

