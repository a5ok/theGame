using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A replica of <see cref="TagAndName"/> for Text3D GameObjects
/// </summary>
public class TagAndNameForMesh : MonoBehaviour
{
    /// <summary>
    /// Reference to the PrefManager
    /// </summary>
    public GameObject PrefManager;
    /// <summary>
    /// A tag that will be added before the name
    /// </summary>
    public string LabelTag;

    void Start()
    {
        if (PrefManager == null)
        {
            PrefManager = GameObject.Find("PrefManager");
        }
        updateName();
    }

    void updateName()
    {
        string name = PrefManager.GetComponent<PrefManager>().actualPlayer.PlayerName;
        gameObject.GetComponent<TextMesh>().text = LabelTag + name;
    }

    void OnEnable()
    {
        Start();
    }
}
