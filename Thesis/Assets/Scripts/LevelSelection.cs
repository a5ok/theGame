using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {

    public static int levelNumber;

    public void SetNumber(int number)
    {
        levelNumber = number;
    }
}
