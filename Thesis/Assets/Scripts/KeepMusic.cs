using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMusic : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("MusicTag").GetComponent<MusicController>().PlayMusic();
    }
}
