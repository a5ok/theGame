using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyTag")
        {
            Destroy(collision.collider);
            GameObject.Find("SessionManager").GetComponent<SessionManager>().AddScore(10);

        }
        
    }
}
