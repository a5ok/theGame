using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip background;
    public AudioClip endLevel;
    public AudioClip gameOver;

    private bool played; // Variables used to don't repeat any sound
   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = background;
        audioSource.Play();
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(EndLevel.hasFinished && !played)
        {
            played = true;
            audioSource.clip = endLevel;
            audioSource.loop = false;
            audioSource.Play();
        }

        if(Death.isDead && !played)
        {
            played = true;
            audioSource.clip = gameOver;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
