﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{

    public int PlayerHealth = 3;
    static public bool isDead = false;
    int damage = 1;
    public GameObject deathTextSx;
    public GameObject deathTextDx;
    [SerializeField] public GameObject textBackGroundSx;
    [SerializeField]  public GameObject textBackGroundDx;
    public Animator myAnimator;
    private PlayerController player;
    public GameObject[] redHearts = new GameObject[3];
    public GameObject[] greyHearts = new GameObject[3];
    private AudioSource audioSource;
    public AudioClip hit;
    public AudioClip potion;


    //used for graphical effects on invulnerability
    Renderer ren;
    Color col;

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        ren = GetComponent<Renderer>();
        col = ren.material.color;
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (PlayerController.playerIsFellDown)
            PlayerDeath();
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "EnemyTag" && PlayerHealth > 1 && !player.isAttacking) // aggiunta variabile per distinzione attacco/morte
        {
            PlayerHealth -= damage;
            audioSource.clip = hit;
            audioSource.Play();
            col.gameObject.SetActive(false);
            StartCoroutine("GetInvulnerable");
           
            if (PlayerHealth == 2)
            {
                redHearts[2].SetActive(false);
                greyHearts[2].SetActive(true);
            }

            else if (PlayerHealth == 1)
            {
                redHearts[1].SetActive(false);
                greyHearts[1].SetActive(true);
            }
        }

        else if (col.gameObject.tag == "EnemyTag" && PlayerHealth == 1 && !player.isAttacking) // aggiunta variabile per distinzione attacco/morte
        {
            PlayerHealth -= damage;
            col.gameObject.SetActive(false);
            PlayerDeath();
        }

        //If the player picks up a potion 
        if (col.gameObject.tag == "PotionTag")
        {

            if (PlayerHealth == 3)
                col.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (PlayerHealth < 3)
            {
                audioSource.clip = potion;
                audioSource.Play();
                PlayerHealth += damage;
                col.gameObject.SetActive(false);

                if (PlayerHealth == 3)
                {
                    redHearts[2].SetActive(true);
                    greyHearts[2].SetActive(false);
                }

                if (PlayerHealth == 2)
                {
                    redHearts[1].SetActive(true);
                    greyHearts[1].SetActive(false);
                }
            }
        }
    }


    //obstacles are trigger
    private void OnTriggerEnter2D(Collider2D col)
    {

        //If the player hits an obstacle
        if (col.gameObject.tag == "ObstacleTag" && PlayerHealth > 1)
        {
            PlayerHealth -= damage;
            audioSource.clip = hit;
            audioSource.Play();
            StartCoroutine("GetInvulnerable");
            if (PlayerHealth == 2)
            {
                redHearts[2].SetActive(false);
                greyHearts[2].SetActive(true);
            }

            else if (PlayerHealth == 1)
            {
                redHearts[1].SetActive(false);
                greyHearts[1].SetActive(true);
            }
        }
        else if (col.gameObject.tag == "ObstacleTag" && PlayerHealth == 1)
        {
            PlayerHealth -= damage;            
            PlayerDeath();
        }
    }

    IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreLayerCollision(8, 12, true); //ignore obstacles
        Physics2D.IgnoreLayerCollision(8, 14, true); //ignore enemies
        col.a = 0.5f;
        ren.material.color = col;
        yield return new WaitForSeconds(.3f);
        col.a = 1f;
        ren.material.color = col;
        yield return new WaitForSeconds(.3f);
        col.a = 0.5f;
        ren.material.color = col;
        yield return new WaitForSeconds(.3f);
        col.a = 1f;
        ren.material.color = col;
        yield return new WaitForSeconds(.3f);
        col.a = 0.5f;
        ren.material.color = col;
        yield return new WaitForSeconds(.3f);
        Physics2D.IgnoreLayerCollision(8, 12, false); //reset obstacles
        Physics2D.IgnoreLayerCollision(8, 14, false); //reset enemies
        col.a = 1f;
        ren.material.color = col;
    }


    void PlayerDeath()
    {
        foreach (GameObject gos in redHearts)
        {
            gos.SetActive(false);
        }

        foreach (GameObject gos in greyHearts)
        {
            gos.SetActive(true);
        }

        isDead = true;
        myAnimator.SetTrigger("Dead");
        deathTextSx.SetActive(true);
        deathTextDx.SetActive(true);
        textBackGroundSx.SetActive(true);
        textBackGroundDx.SetActive(true);


    }
}