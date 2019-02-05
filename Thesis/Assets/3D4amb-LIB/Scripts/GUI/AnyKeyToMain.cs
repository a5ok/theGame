using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyToMain : MonoBehaviour
{
    public string mainSceneName;

	void Update ()
    {
		if(Input.anyKey)
        {
            SceneManager.LoadScene(mainSceneName);
        }
	}
}
