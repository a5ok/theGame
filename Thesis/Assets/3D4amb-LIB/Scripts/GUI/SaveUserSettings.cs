using Assets._3D4amb_LIB;
using Assets._3D4amb_LIB.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

    public static GameDifficulty difficulty;
    public static Eye eye;

    private string email;
    private string password;
    private string accountName;
    private string eyeSelected;
    private string difficultySelected;
    private string playerSettings;
    private string url;
    private string result;


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
        eye = setEye;
        difficulty = setDiff;
       
        StoreSettingsOnline();
    }

    public void StoreSettingsOnline()
    {
        email = Login.email;
        password = Login.passwordEncoded;
        accountName = AccountButtons.onlineName;
        eyeSelected = GetEye().ToString();
        difficultySelected = GetDifficulty().ToString();
        PlayerSettingsOnline settings = new PlayerSettingsOnline
        {
            difficulty = difficultySelected,
            eye = eyeSelected
        };
        playerSettings = settings.SaveToJson();
        url = "https://se4med.unibg.it/se4medservice/?action=storesettings&useremail=" + email + "&password=" + password + "&username=" + accountName + "&idapp=Runeye&appsettings=" + playerSettings;
        result = Get(url);
    }

    public int GetEye()
    {
        if (eye == Eye.LEFT)
            return 1;
        else
            return 0;
    }

    public int GetDifficulty()
    {
        if (difficulty == GameDifficulty.EASY)
            return 0;
        else if (difficulty == GameDifficulty.MEDIUM)
            return 1;
        else
            return 2;
    }

    public string Get(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}
