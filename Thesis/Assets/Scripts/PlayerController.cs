using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    public float hitForce;
    private Rigidbody2D myRigidbody;
    private bool grounded;
    private bool playerIsFalling;
    public static bool playerIsFellDown;
    private float currentFallTime;
    public float maxFallTime;
    public bool isAttacking; //aggiunto per distinguere la collisione da attacco da quella di morte
    private AudioSource audioSource;
    public AudioClip swordAttack;
    public AudioClip jump;
    public AudioClip coin;
    public LayerMask whatIsGround;
    private BoxCollider2D myCollider;
    public GameObject playerSword;
    private Animator myAnimator;


    private void Awake()
    {
        Death.isDead = false;
    }

    // Use this for initialization
    void Start () {

        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isAttacking = false;
        playerIsFellDown = false;
    }
	
	// Update is called once per frame
	void Update () {

        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myAnimator.SetBool("Ground", grounded);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        // Player jumps when X button is clicked
        if (grounded && Input.GetButtonDown("Jump"))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            audioSource.clip = jump;
            audioSource.Play();
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetFloat("vSpeed", myRigidbody.velocity.y);

        //Player attacks when A button is clicked
        if (grounded && Input.GetButtonDown("Fire1") && !Death.isDead)
            StartCoroutine(Attack());

        //Check if player is grounded or not
        if (!grounded)
            playerIsFalling = true;
        else
        {
            currentFallTime = 0;
            playerIsFalling = false;
        }

        if (playerIsFalling)
            currentFallTime = currentFallTime + Time.deltaTime;

        if (currentFallTime > maxFallTime)
            playerIsFellDown = true;

        
        //Player dies when loses all lives
        if (Death.isDead)
        {
           myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);

            if (Input.GetButtonDown("Fire1"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else if (Input.GetButtonDown("Jump"))
                SceneManager.LoadScene("MainMenu");


        }

        if (EndLevel.hasFinished)
        {
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
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
        audioSource.clip = swordAttack;
        audioSource.Play();
        yield return new WaitForSeconds(.2f);
        playerSword.SetActive(false);
        isAttacking = false;
    }

    //Player pickups a coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("CoinTag")) // aggiunta la variabile di distinzione
        {
            audioSource.PlayOneShot(coin, 0.5f);
            collision.gameObject.SetActive(false);
            GameObject.Find("SessionManager").GetComponent<SessionManager>().AddScore(1);
        }

    }




}
