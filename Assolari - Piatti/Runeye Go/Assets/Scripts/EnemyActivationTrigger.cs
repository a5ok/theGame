using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationTrigger : MonoBehaviour
{
    private EnemyController thisEnemy;

    private void Awake()
    {
        thisEnemy = GetComponent<EnemyController>();
        thisEnemy.isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerTag")
            thisEnemy.isActive = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerTag")
            thisEnemy.isActive = false;
    }
}
