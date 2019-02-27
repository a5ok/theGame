using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script let you load a scene by index
/// </summary>
public class LoadSceneOnClick : MonoBehaviour
{
    /// <summary>
    /// Load a scene from its index in the SceneManager
    /// </summary>
    /// <param name="sceneName">An acceptable name for a Scene</param>
    public void LoadByName(string sceneName)
    {
        StartCoroutine(DelaySceneLoad(sceneName));
    }

    IEnumerator DelaySceneLoad(string level)
    {
        yield return new WaitForSeconds(.3f);
        SceneManager.LoadScene(level);
    }

}
