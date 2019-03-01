using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountButtons : MonoBehaviour
{
    public GameObject button;
    public GameObject panel;
    public static string onlineName;
   
    void Awake() {

        int numberOfButtons = Login.accounts.Length;

        for(int i = 0; i < numberOfButtons; i++)
        {
            GameObject go = Instantiate(button);
            go.transform.SetParent(panel.transform);
            go.GetComponentInChildren<Text>().text = Login.accounts[i];
            go.GetComponent<Button>().onClick.AddListener(() => onlineNameSelection(go.GetComponentInChildren<Text>().text));
        }
    }

    public void onlineNameSelection(string name)
    {
        onlineName = name;
    }

}
