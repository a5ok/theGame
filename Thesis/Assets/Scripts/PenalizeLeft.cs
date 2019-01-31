using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenalizeLeft : MonoBehaviour
{

    GameObject[] enemies;
    public static float opacity;
    public static bool penalize;
    public static int triggerCounter;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        penalize = false;
        triggerCounter = 0;
    }

    private void OnPreRender()
    {


        if (penalize) {

            foreach (GameObject enemy in enemies)
            {
                if (EyeSelection.eye == "DESTRO")
                    enemy.GetComponent<Renderer>().material.color = new Color(1, 1, 1, opacity);
                else
                    enemy.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            }
        }

    }

    private void OnPostRender()
    {
        foreach (GameObject enemy in enemies)
        {
            if (EyeSelection.eye == "DESTRO")
                enemy.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }

    }
}
