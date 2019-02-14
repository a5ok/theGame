using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public static bool hit; // variabile usata per gestire il colpo subito e quindi l'attivazione del particle system
    public ParticleSystem particles;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myCollider;
    private SpriteRenderer mySprite;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (hit)
            Explode();

    }

    public void Explode()
    {
        ParticleSystem ps = Instantiate(particles, transform.position, Quaternion.identity);
        ps.Play();
        myCollider.enabled = false;
        mySprite.enabled = false;
        Destroy(gameObject, 4f);
        hit = false;

    }




}
