using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to draw the penalty transparency.
/// </summary> 
/// <remarks>Attach me to just one camera, the Left one for example</remarks>
public class DrawEyePatch : MonoBehaviour
{
    /// <summary>
    /// Black texture that represents the black EyePatch
    /// </summary>
    public Texture2D EyePatch;
    /// <summary>
    /// The Penalty manager that sends the informations of the current
    /// penalty at the Camera
    /// </summary>
    public PenaltyManager PM;

    void Awake()
    {
        while(PM==null)
        {
            PM = GameObject.Find("PenaltyManager").GetComponent<PenaltyManager>();
        }
    }

    void OnGUI()
    {
        if(PM!=null)
        {
            GUI.color = new Color(0, 0, 0, PM.PenaltyInfoNow.PenaltyEyePatch);
            if (PM.PenaltyInfoNow.PenaltyEye == Eye.LEFT)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width / 2, Screen.height), EyePatch);
            }
            else
            {
                GUI.DrawTexture(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height), EyePatch);
            }
        }
    }
}
