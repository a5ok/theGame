using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class LoadOnlineSettings : MonoBehaviour
{
    private string email;
    private string accountName;
    private string url;
    private string result;

    public void Load()
    {
        email = Login.email;
        accountName = AccountButtons.onlineName;
        url = "https://se4med.unibg.it/se4medservice/?action=getsettings&useremail=" + email + "&username=" + accountName + "&idapp=Runeye";
        Debug.Log(url);
        result = Get(url);
        Debug.Log(result);
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
