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


    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
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
            col.gameObject.SetActive(false);
           
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
    }



    void PlayerDeath()
    {
        foreach(GameObject gos in redHearts)
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