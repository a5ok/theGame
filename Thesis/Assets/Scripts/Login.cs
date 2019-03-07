using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public GameObject InputFieldEmail;
    public GameObject InputFieldPassword;
    public GameObject loginText;
    public GameObject failText;
    public static string email;
    public static string passwordEncoded;
    private string url;
    private static string result;
    private string loginFailed = "Served at: /se4medservice{authenticationPatient=Authentication failed. Wrong credential or no user found.}";
    public static string[] accounts;

    public void LoginAttempt()
    {
        email = InputFieldEmail.GetComponent<InputField>().text;
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

        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
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
            StartCoroutine(Retry());
        else
            //Debug.Log(result);
            GetAccounts(result);

        
    }

    public void GetAccounts(string result)
    {
        int lenght = result.Length;
        string accountsString = result.Substring(48, lenght - 49);
        Debug.Log(accountsString);
        accounts = accountsString.Split(',');
        SceneManager.LoadScene("AccountSelection");
    }

    public IEnumerator Retry()
    {
        loginText.SetActive(false);
        failText.SetActive(true);
        yield return new WaitForSeconds(3f);
        failText.SetActive(false);
        loginText.SetActive(true);

    }

    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain, look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }



}
