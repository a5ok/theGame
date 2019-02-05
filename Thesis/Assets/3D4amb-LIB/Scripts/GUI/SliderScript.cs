using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base class for the Slider scripts
/// </summary>
public class SliderScript : MonoBehaviour
{
    /// <summary>
    /// Label that change with the slider
    /// </summary>
    public GameObject ItsLabel;
    /// <summary>
    /// A reference to the PrefManager
    /// </summary>
    public GameObject PrefManager;

    /// <summary>
    /// A tag that will be print before the value of the slider
    /// </summary>
    protected string SliderTag;
    protected string text;
    protected float v;

    void OnEnable()
    {
        SetTag();
        InitSliderValue();
        UpdateSlider();
    }

    protected virtual void SetTag()
    {
    }
    
    protected virtual void InitSliderValue()
    {
    }

    public virtual void UpdateSlider()
    {
        v = gameObject.GetComponent<Slider>().value;
    }

    protected virtual void SetText(string val)
    {
        text = SliderTag + val;
        ItsLabel.GetComponent<Text>().text = text;
    }



}

