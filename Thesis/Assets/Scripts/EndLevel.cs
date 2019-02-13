using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    static public bool hasFinished;
    public GameObject endText;
    // Start is called before the first frame update
    void Start()
    {
        hasFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerTag")
        {
            print("fine");
            hasFinished = true;
            endText.SetActive(true);
        }
    }
}
