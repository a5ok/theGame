using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyToMain : MonoBehaviour
{
    public string mainSceneName;

    private void Start()
    {
        switch(LevelSelection.levelNumber)
        {
            case 0:
                mainSceneName = "Tutorial";
                break;
            case 1:
                mainSceneName = "Level 1";
                break;
            case 2:
                mainSceneName = "Level 2";
                break;
            case 3:
                mainSceneName = "Level 3";
                break;
            case 4:
                mainSceneName = "Level 4";
                break;
        }
    }

    void Update ()
    {
		if(Input.anyKey)
        {
            SceneManager.LoadScene(mainSceneName);
        }
	}


}
