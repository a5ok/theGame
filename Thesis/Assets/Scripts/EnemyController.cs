using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public bool isActive;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
       
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
            myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        else
            myRigidbody.velocity = new Vector2(0, 0);

    }

}
