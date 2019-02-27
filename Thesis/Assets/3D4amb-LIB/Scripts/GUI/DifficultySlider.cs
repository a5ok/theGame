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
    PrefManager pm;

    private void Awake()
    {
        pm = GameObject.Find("PrefManager").GetComponent<PrefManager>();
    }

    protected override void SetTag()
    {
        SliderTag = "Difficulty: ";
    }

    protected override void InitSliderValue()
    {
        gameObject.GetComponent<Slider>().value = (int)pm.GetComponent<PrefManager>().actualPlayerSettings.difficultyStart;
    }

	public override void UpdateSlider()
    {
        base.UpdateSlider();
        string val = ((GameDifficulty)(int)v).ToString();
        base.SetText(val);
    }
}
