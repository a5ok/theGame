using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this to the slider for the Difficulty
/// </summary>
public class DifficultySlider : SliderScript   
{
    protected override void SetTag()
    {
        SliderTag = "Difficulty: ";
    }

    protected override void InitSliderValue()
    {
        gameObject.GetComponent<Slider>().value = (int)PrefManager.GetComponent<PrefManager>().actualPlayerSettings.difficultyStart;
    }

	public override void UpdateSlider()
    {
        base.UpdateSlider();
        string val = ((GameDifficulty)(int)v).ToString();
        base.SetText(val);
    }
}
