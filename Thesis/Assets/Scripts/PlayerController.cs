using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D myRigidbody;
    private bool grounded;
    public LayerMask whatIsGround;
    private Collider2D myCollider;
    private Animator myAnimator;

	// Use this for initialization
	void Start () {
        myCollider = GetComponent<Collider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () { 

        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);


        // Il player salta quando viene toccato lo schermo
        if (grounded && Input.GetKeyDown(KeyCode.Space))
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);



    }
}
