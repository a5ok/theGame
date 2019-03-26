using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script must be attached to both cameras
/// </summary>
public class CameraPenalizer : MonoBehaviour
{
    /// <summary>
    /// This must be set LEFT on the Main Camera Left, RIGHT on the other one
    /// </summary>
    /// <remarks>Should be already set if you are using the prefabs</remarks>
    public Eye eye;
    /// <summary>
    /// The Penalty manager that sends the informations of the current
    /// penalty at the Camera
    /// </summary>
    public PenaltyManager PM;
    /// <summary>
    /// The list of the GameObject that must be penalized
    /// </summary>
    public List<GameObject> PenalizablesGos;

    /// <summary>
    /// This add the penalizable Objects that are already in the scene
    /// at start
    /// </summary>
    void Start()
    {
        while (PM == null)
        {
            PM = GameObject.Find("PenaltyManager").GetComponent<PenaltyManager>();
        }
        PenalizablesGos = new List<GameObject>();
        foreach (string tag in PM.TagToPenalize)
        {
            GameObject[] arr;
            arr = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject go in arr)
            {
                PenalizablesGos.Add(go);
            }
        }
    }

    /// <summary>
    /// This method add the GameObject to the list of 
    /// penalizable gameObjects if it should
    /// </summary>
    /// <remarks>Here it's done the check on the tag</remarks>
    /// <param name="go">The GameObject that could be added to the Penalizable list, according to its tag</param>
    public void AddIfPenalizable(GameObject go)
    {
        bool tagIsToPenalize = false;
        foreach (string tag in PM.TagToPenalize)
        {
            if(tag.Equals(go.tag))
            {
                tagIsToPenalize = true;
                break;
            }
        }
        if(PM!=null && tagIsToPenalize)
        {
            PenalizablesGos.Add(go);    
        }
    }
 
    /// <summary>
    /// This is where the penalty is done over the GameObjects
    /// </summary>
    void OnPreRender()
    {   
        if (PM.PenaltyInfoNow.PenaltyEye == eye)
        {
            foreach (GameObject go in PenalizablesGos)
            {
                if(go!=null)
                {
                    go.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1 - PM.PenaltyInfoNow.PenaltyTransparency);
                }
            }
        }
        else
        {
            foreach (GameObject go in PenalizablesGos)
            {
                if (go != null)
                {
                    go.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
                }
            }
        }
    }

    /// <summary>
    /// This part is done after the Camera has rendered the scene.
    /// It checks if some of the penalizable objects have been destroyed
    /// and empties the list accordingly
    /// </summary>
    void OnPostRender()
    {
        List<GameObject> notNulls = new List<GameObject>();
        foreach(GameObject go in PenalizablesGos)
        {
            if (go != null) notNulls.Add(go);
        }
        PenalizablesGos = notNulls;
    }
}
