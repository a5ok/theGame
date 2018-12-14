using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTutorial : MonoBehaviour {

    [SerializeField] GameObject text;
    private bool triggered;
    private bool firstTime;
    public LayerMask whatIsPlayer;
    private Collider2D myCollider;

    // Use this for initialization
	void Start () {

        firstTime = true;
        myCollider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

        triggered = Physics2D.IsTouchingLayers(myCollider, whatIsPlayer);

        if(triggered && firstTime)
        {
            text.SetActive(true);
            firstTime = false;
            Time.timeScale = 0f;

        }

        if(triggered && !firstTime && Input.GetKeyDown(KeyCode.Space))
        {
            text.SetActive(false);
            Time.timeScale = 1f;
        }

            

    }
}
