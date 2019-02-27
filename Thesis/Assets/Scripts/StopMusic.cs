using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("MusicTag").GetComponent<MusicController>().StopMusic();
    }
}
