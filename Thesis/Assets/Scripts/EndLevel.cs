using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public static bool hasFinished;
    public GameObject endTextSx;
    public GameObject endTextDx;
    public GameObject textBackGroundSx;
    public GameObject textBackGroundDx;
    public Animator myAnimator;
    public static bool endLevel;

    // Start is called before the first frame update
    void Start()
    {
        hasFinished = false;
        endLevel = false;
    }

    //When the player enters the EndLevel collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerTag")
        {
            myAnimator.SetTrigger("Win");
            endLevel = true;
            hasFinished = true;
            endTextSx.SetActive(true);
            endTextDx.SetActive(true);
            textBackGroundSx.SetActive(true);
            textBackGroundDx.SetActive(true);
        }

    }

   
}


