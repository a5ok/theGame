using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private PlayerController player;
    private Collider2D swordAttack;
    public ParticleSystem particles;
    private AudioSource audioSource;
    public AudioClip enemyGrunt;

    void Awake() 
    {
        player = GetComponentInParent<PlayerController>();
        swordAttack = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyTag" && player.isAttacking)
        {
            ParticleSystem ps = Instantiate(particles, transform.position, Quaternion.identity);
            ps.Play();

            audioSource.PlayOneShot(enemyGrunt);
            collision.collider.GetComponent<BoxCollider2D>().enabled = false;
            collision.collider.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(collision.collider.gameObject, 4f);
            GameObject.Find("SessionManager").GetComponent<SessionManager>().AddScore(10);


        }
        
    }

}
