using Assets._3D4amb_LIB;
using Assets._3D4amb_LIB.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This object let you save the user settings 
/// read from GUI objects in the canvas
/// </summary>
public class SaveUserSettings : MonoBehaviour
{
    /// <summary>
    /// Slider for the difficulty
    /// </summary>
    public GameObject difficultySlider;
    /// <summary>
    /// Slider for the healty eye
    /// </summary>
    public GameObject eyeSlider;
    /// <summary>
    /// Reference to the PrefManager
    /// </summary>
    public GameObject PrefManager;

    void Awake()
    {
        if (PrefManager == null)
        {
            PrefManager = GameObject.Find("PrefManager");
        }
    }


    /// <summary>
    /// Read the data from the Gamebject and save the
    /// Player settings in the PrefManager PlayerPrefs
    /// </summary>
    public void SaveSettings()
    {
        GameDifficulty setDiff  = (GameDifficulty)difficultySlider.GetComponent<Slider>().value;
        Eye setEye              = (Eye)eyeSlider.GetComponent<Slider>().value;
        PrefManager.GetComponent<PrefManager>().SavePlayerSettings(setDiff, setEye);
        PrefManager.GetComponent<PrefManager>().LoadPlayerSettings();   //so it also loads the player settings just saved
    }
}
