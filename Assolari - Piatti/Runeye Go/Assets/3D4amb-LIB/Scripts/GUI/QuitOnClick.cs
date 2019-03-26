using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Exit the application on click
/// </summary>
public class QuitOnClick : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
