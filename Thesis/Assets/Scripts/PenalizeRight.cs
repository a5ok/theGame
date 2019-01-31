using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenalizeRight : MonoBehaviour
{

    GameObject[] enemies;
    public static float opacity;
    public static bool penalize;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        penalize = false;
    }

    private void OnPreRender()
    {
        if (penalize)
        {
            foreach (GameObject enemy in enemies)
            {
                if (EyeSelection.eye == "SINISTRO")
                    enemy.GetComponent<Renderer>().material.color = new Color(1, 1, 1, opacity);
                else
                    enemy.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            }
        }

    }

    private void OnPostRender()
    {
        foreach (GameObject enemy in enemies) {
            if (EyeSelection.eye == "SINISTRO")
                enemy.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }

    }
}