using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to the Left camera to draw the VR mask
/// (the rectangular with black borders and the hexagonal space for the 2 images)
/// </summary>
public class DrawVrMask : MonoBehaviour
{
    /// <summary>
    /// The VrMask image
    /// </summary>
    public Texture2D VrMask;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), VrMask);
    }
}
