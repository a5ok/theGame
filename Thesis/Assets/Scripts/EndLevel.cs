using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    static public bool hasFinished;
    public GameObject endTextSx;
    public GameObject endTextDx;
    public Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        hasFinished = false;
    }

    //When the player enters the EndLevel collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerTag")
        {
            myAnimator.SetTrigger("Win");
            hasFinished = true;
            endTextSx.SetActive(true);
            endTextDx.SetActive(true);
        }
    }

   
}
