using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using Assets._3D4amb_LIB;

public class AccountButtons : MonoBehaviour
{
    public GameObject PrefManager;
    public GameObject button;
    public GameObject panel;
    public static string onlineName;
    private string email;
    private string accountName;
    private string url;
    private string result;
    private string noSettingsFound = "Served at: /se4medserviceNo settings found";

    void Awake() {

        int numberOfButtons = Login.accounts.Length;

        for(int i = 0; i < numberOfButtons; i++)
        {
            GameObject go = Instantiate(button);
            go.transform.SetParent(panel.transform);
            go.GetComponentInChildren<Text>().text = Login.accounts[i];
            go.GetComponent<Button>().onClick.AddListener(() => OnlineNameSelection(go.GetComponentInChildren<Text>().text));
            go.GetComponent<Button>().onClick.AddListener(() => LoadSettingsOnline(go.GetComponentInChildren<Text>().text));
        }

        if (PrefManager == null)
        {
            PrefManager = GameObject.Find("PrefManager");
        }
    }

    public void OnlineNameSelection(string name)
    {
        onlineName = name;
    }

    public void LoadSettingsOnline(string name)
    {
        email = Login.email;
        accountName = name;
        url = "https://se4med.unibg.it/se4medservice/?action=getsettings&useremail=" + email + "&username=" + accountName + "&idapp=Runeye";
        //Debug.Log(url);
        result = Get(url);
        //Debug.Log(result);
        if (result != noSettingsFound)
        {

            GameDifficulty difficulty = GetDifficulty(result);
            Eye eye = GetEye(result);
            //Debug.Log(difficulty);
            //Debug.Log(eye);
            PrefManager.GetComponent<PrefManager>().SavePlayerSettings(difficulty, eye);
            PrefManager.GetComponent<PrefManager>().LoadPlayerSettings();
        }
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

    public GameDifficulty GetDifficulty(string s)
    {
        string diffSelected = s.Substring(40, 1);
        if (diffSelected == "0")
            return GameDifficulty.EASY;
        else if (diffSelected == "1")
            return GameDifficulty.MEDIUM;
        else
            return GameDifficulty.HARD;

    }

    public Eye GetEye(string s)
    {
        string eyeSelected = s.Substring(50, 1);
        if (eyeSelected == "0")
            return Eye.RIGHT;
        else
            return Eye.LEFT;
    }


}
