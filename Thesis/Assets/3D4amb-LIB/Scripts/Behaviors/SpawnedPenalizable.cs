using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only GameObjects that are not present in the scene when it's loaded
/// need this script attached to them. 
/// </summary>
public class SpawnedPenalizable : MonoBehaviour {

    void Awake()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach (GameObject c in cameras)
        {
            c.GetComponent<CameraPenalizer>().AddIfPenalizable(this.gameObject);
        }
	}
}
