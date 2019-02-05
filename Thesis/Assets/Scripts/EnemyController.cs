using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myCollider;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
    }

   
}
