using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public bool isActive;
    private Rigidbody2D myRigidbody;
    private AudioSource audioSource;
    public AudioClip enemyGrunt;

    // Start is called before the first frame update
    void Start()
    {   
        myRigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
            myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        else
            myRigidbody.velocity = new Vector2(0, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "SwordTag")
        {
            audioSource.clip = enemyGrunt;
            audioSource.Play();
        }
    }


}
