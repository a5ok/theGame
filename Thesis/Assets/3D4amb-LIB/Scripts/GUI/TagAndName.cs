using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A simple script that update a label with a tag (set this from the inspector)
/// and the name of the actualPlayer
/// </summary>
public class TagAndName : MonoBehaviour
{
    /// <summary>
    /// Reference to the PrefManager
    /// </summary>
    public GameObject PrefManager;
    /// <summary>
    /// A tag that will be added before the name
    /// </summary>
    public string LabelTag;

	void Start ()
    {
        if(PrefManager==null)
        {
            PrefManager = GameObject.Find("PrefManager");
        }
        updateName();
	}

    /// <summary>
    /// Update the text with LabelTag + ActualPlayer.PlayerName
    /// </summary>
    void updateName()
    {
        string name = PrefManager.GetComponent<PrefManager>().actualPlayer.PlayerName;
        gameObject.GetComponent<Text>().text = LabelTag + name;
    }

    void OnEnable()
    {
        Start();
    }
	
}
