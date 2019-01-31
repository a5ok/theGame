using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMask : MonoBehaviour
{
    [SerializeField] Texture mask;

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mask);
    }
}
