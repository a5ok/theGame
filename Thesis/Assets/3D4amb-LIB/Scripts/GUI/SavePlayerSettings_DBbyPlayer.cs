using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script manages the saving of player settings read
/// from the sliders in the panel in the MainMenu
/// </summary>
public class SavePlayerSettings_DBbyPlayer : MonoBehaviour
{
    /// <summary>
    /// The PrefManager where the PlayerSettings will be saved
    /// </summary>
    public GameObject PrefManager;
    /// <summary>
    /// Slider of the healty eye
    /// </summary>
    public GameObject HealtyEyeSlider;
    /// <summary>
    /// Slider of the transparency at start
    /// </summary>
    public GameObject TraspStartSlider;
    /// <summary>
    /// Slider of the transparency at end
    /// </summary>
    public GameObject TraspEndSlider;
    /// <summary>
    /// Slider of the eye-patch at start
    /// </summary>
    public GameObject EyePStartSlider;
    /// <summary>
    /// Slider of the eye-patch at end
    /// </summary>
    public GameObject EyePEndSlider;
    /// <summary>
    /// Content of the scrollview into the Panel
    /// </summary>
    public GameObject ScrollViewContent;

    private PrefManager prefManager;
    private Slider traspStartSlider;
    private Slider traspEndSlider;
    private Slider eyePStartSlider;
    private Slider eyePEndSlider;
    private Slider healtyEyeSlider;
    private Vector3 anchoredPosition;

    void Start()
    {
        anchoredPosition = ScrollViewContent.GetComponent<RectTransform>().position;
    }

    void OnEnable()
    {
        prefManager = PrefManager.GetComponent<PrefManager>();
        traspStartSlider = TraspStartSlider.GetComponent<Slider>();
        traspEndSlider = TraspEndSlider.GetComponent<Slider>();
        eyePStartSlider = EyePStartSlider.GetComponent<Slider>();
        eyePEndSlider = EyePEndSlider.GetComponent<Slider>();
        healtyEyeSlider = HealtyEyeSlider.GetComponent<Slider>();
    }

    /// <summary>
    /// Read the Penalty settings from all the sliders in the menu
    /// and save it into the PrefManager as PenaltyInfoStart and PenaltyInfoEnd
    /// </summary>
    public void ReadAndSave()
    {
        Eye eye = (Eye)(int)healtyEyeSlider.value;
        PenaltyInfo PIstart = new PenaltyInfo(  eye,
                                                traspStartSlider.value,
                                                eyePStartSlider.value
                                                );
        PenaltyInfo PIend = new PenaltyInfo(    eye,
                                                traspEndSlider.value,
                                                eyePEndSlider.value
                                                );
        prefManager.SavePlayerPIstart(PIstart);
        prefManager.SavePlayerPIend(PIend);
        ScrollViewContent.GetComponent<RectTransform>().position = anchoredPosition;
    }
	
}
