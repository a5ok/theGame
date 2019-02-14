using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D myRigidbody;
    private bool grounded;
    public bool isAttacking; //aggiunto per distinguere la collisione da attacco da quella di morte
    public LayerMask whatIsGround;
    private BoxCollider2D myCollider;
    public GameObject playerSword;
    private Animator myAnimator;
   

	// Use this for initialization
	void Start () {

        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        isAttacking = false;

    }
	
	// Update is called once per frame
	void Update () {

        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myAnimator.SetBool("Ground", grounded);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        // Player jumps when X button is clicked
        if (grounded && Input.GetButtonDown("Jump"))
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetFloat("vSpeed", myRigidbody.velocity.y);

        //Player attacks when A button is clicked
        if (grounded && Input.GetButtonDown("Fire1"))
            StartCoroutine(Attack());

        //Player dies when loses all lives
        if (Death.isDead)
        {
            myRigidbody.velocity = new Vector2(0, 0);
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Death.isDead = false;
            }
            else if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("MainMenu");
                Death.isDead = false;
               
            }
        }

        if (EndLevel.hasFinished)
        {
            myRigidbody.velocity = new Vector2(0, 0);
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                EndLevel.hasFinished = false;
                GameObject.Find("SessionManager").GetComponent<SessionManager>().SaveSession();
            }
            else if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("MainMenu");
                EndLevel.hasFinished = false;
                GameObject.Find("SessionManager").GetComponent<SessionManager>().SaveSession();

            }
        }


        
    }

    public IEnumerator Attack() // aggiunta variabile per distinzione attacco/morte
    {
        playerSword.SetActive(true);
        myAnimator.SetTrigger("Attack");
        isAttacking = true;
        yield return new WaitForSeconds(.5f);
        playerSword.SetActive(false);
        isAttacking = false;
    }

    //Player pickups a coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CoinTag") && !isAttacking) // aggiunta la variabile di distinzione
        {
            collision.gameObject.SetActive(false);
            GameObject.Find("SessionManager").GetComponent<SessionManager>().AddScore(1);
        }

    }


}
