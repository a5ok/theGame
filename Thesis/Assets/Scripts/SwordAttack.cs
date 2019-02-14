using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private PlayerController player;
    private Collider2D swordAttack;

    void Awake() 
    {
        player = GetComponentInParent<PlayerController>();
        swordAttack = GetComponent<Collider2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyTag" && player.isAttacking)
        {
            EnemyController.hit = true;
            GameObject.Find("SessionManager").GetComponent<SessionManager>().AddScore(10);

        }
        
    }
}
