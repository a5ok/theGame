using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentageSlider : SliderScript
{
    /// <summary>
    /// Set this in the inspector
    /// </summary>
    public string Tag;

    protected override void SetTag()
    {
        SliderTag = Tag;
    }

    public override void UpdateSlider()
    {
        base.UpdateSlider();
        string val = (int)(v*100) + "%";
        base.SetText(val);
    }
}
