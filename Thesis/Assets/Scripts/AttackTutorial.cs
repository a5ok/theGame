using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutorial : MonoBehaviour
{
    [SerializeField] GameObject textSx;
    [SerializeField] GameObject textDx;
    private bool triggered;
    private bool firstTime;
    public LayerMask whatIsPlayer;
    private Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        triggered = Physics2D.IsTouchingLayers(myCollider, whatIsPlayer);

        if (triggered && firstTime)
        {
            textSx.SetActive(true);
            textDx.SetActive(true);
            firstTime = false;
            Time.timeScale = 0f;

        }

        if (triggered && !firstTime && Input.GetKeyDown(KeyCode.Space))
        {
            textSx.SetActive(false);
            textDx.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
