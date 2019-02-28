using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public GameObject InputFieldEmail;
    public GameObject InputFieldPassword;
    private string passwordEncoded;
    private string url;

    public void LoginAttempt()
    {
        string email = InputFieldEmail.GetComponent<InputField>().text;
        string password = InputFieldPassword.GetComponent<InputField>().text;
        passwordEncoded = HashFunction.Sha256(password);
        url = "https://se4med.unibg.it/se4medservice/?action=authenticatepatient&useremail=" + email + "&password=" + passwordEncoded + "&idapp=Runeye";
    }



}
