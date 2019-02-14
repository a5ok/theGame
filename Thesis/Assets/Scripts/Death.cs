using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{

    public int PlayerHealth = 3;
    static public bool isDead = false;
    int damage = 1;
    public GameObject deathText;
    public Animator myAnimator;
    private PlayerController player;


    public GameObject[] redHeartsSx = new GameObject[3];
    public GameObject[] redHeartsDx = new GameObject[3];
    public GameObject[] greyHeartsSx = new GameObject[3];
    public GameObject[] greyHeartsDx = new GameObject[3];


    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "EnemyTag" && PlayerHealth > 1 && !player.isAttacking) // aggiunta variabile per distinzione attacco/morte
        {
            PlayerHealth -= damage;
            col.gameObject.SetActive(false);
            print(PlayerHealth);

            if (PlayerHealth == 2)
            {
                redHeartsSx[2].SetActive(false);
                redHeartsDx[2].SetActive(false);
                greyHeartsSx[2].SetActive(true);
                greyHeartsDx[2].SetActive(true);
            }

            else if (PlayerHealth == 1)
            {
                redHeartsSx[1].SetActive(false);
                redHeartsDx[1].SetActive(false);
                greyHeartsSx[1].SetActive(true);
                greyHeartsDx[1].SetActive(true);
            }
        }

        else if (col.gameObject.tag == "EnemyTag" && PlayerHealth == 1 && !player.isAttacking) // aggiunta variabile per distinzione attacco/morte
        {
            PlayerHealth -= damage;
            col.gameObject.SetActive(false);
            print(PlayerHealth);
            redHeartsSx[0].SetActive(false);
            redHeartsDx[0].SetActive(false);
            greyHeartsSx[0].SetActive(true);
            greyHeartsDx[0].SetActive(true);
            isDead = true;
            myAnimator.SetTrigger("Dead");
            print("boolean a true");
            deathText.SetActive(true);
        }
    }
}