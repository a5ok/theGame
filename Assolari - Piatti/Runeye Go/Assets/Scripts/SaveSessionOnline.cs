using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class SaveSessionOnline : MonoBehaviour
{
    private string accountName;
    private string level;
    private string score;
    private string eye;
    private string difficulty;
    private string results;
    private string url;


    public void Save()
    {
        accountName = AccountButtons.onlineName;
        level = GetLevel();
        score = GetScore();
        eye = GetEye();
        difficulty = GetDifficulty();
        results = " " + level + " - " + score + " - " + eye + " - " + difficulty;
        url = "https://se4med.unibg.it/se4medservice/?action=storeresults&useremail=" + Login.email + "&password=" + Login.passwordEncoded + "&username=" + accountName + "&idapp=Runeye&result=" + results;
        Get(url);
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

    public string GetLevel()
    {
        int levelNumber = GameObject.Find("SessionManager").GetComponent<SessionManager>().GetLevel();
        string currentLevel = "Level number: " + levelNumber;
        return currentLevel;
    }

    public string GetScore()
    {
        int scoreNumber = GameObject.Find("SessionManager").GetComponent<SessionManager>().GetScore();
        string currentScore = "Score: " + scoreNumber;
        return currentScore;
    }

    public string GetEye()
    {
        string amblyopicEye;
        Eye currentEye = SaveUserSettings.eye;

        if(currentEye == Eye.LEFT)
            amblyopicEye = "Amblyopic eye: right";
        else 
            amblyopicEye = "Amblyopic eye: left";

        return amblyopicEye;
    }

    public string GetDifficulty()
    {
        string difficultySelected;
        GameDifficulty currentDifficulty = SaveUserSettings.difficulty;

        if (currentDifficulty == GameDifficulty.EASY)
            difficultySelected = "Difficulty: easy";
        else if (currentDifficulty == GameDifficulty.MEDIUM)
            difficultySelected = "Difficulty: medium";
        else
            difficultySelected = "Difficulty: hard";

        return difficultySelected;
    }
}
