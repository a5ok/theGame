using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public GameObject InputFieldEmail;
    public GameObject InputFieldPassword;
    private string passwordEncoded;
    private string url;
    private static string result;
    private string loginFailed = "Served at: /se4medservice{authenticationPatient=Authentication failed. Wrong credential or no user found.}";

    public void LoginAttempt()
    {
        string email = InputFieldEmail.GetComponent<InputField>().text;
        string password = InputFieldPassword.GetComponent<InputField>().text;
        passwordEncoded = HashFunction.Sha256(password);
        url = "https://se4med.unibg.it/se4medservice/?action=authenticatepatient&useremail=" + email + "&password=" + passwordEncoded + "&idapp=Runeye";
        result = Get(url);
        CheckInfos(result);
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

    public void CheckInfos(string result)
    {
        if (result.Equals(loginFailed))
            Debug.Log("NOPE");
        
    }



}
