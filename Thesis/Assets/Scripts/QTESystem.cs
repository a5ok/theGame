using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour {

    [SerializeField] GameObject QTEText;
    [SerializeField] GameObject qKey;
    [SerializeField] GameObject wKey;
    [SerializeField] GameObject eKey;
    private int countingDown;
    private int keyToPress;
    private int correctKey;
    private bool triggered;
    private bool firstTime;
    public LayerMask whatIsPlayer;
    private Collider2D myCollider;

    // Use this for initialization
    void Start () {

        keyToPress = Random.Range(1, 4);
        firstTime = true;
        myCollider = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {

        triggered = Physics2D.IsTouchingLayers(myCollider, whatIsPlayer);

        if (triggered && firstTime)
        {
            Time.timeScale = 0f;
            countingDown = 1;
            StartCoroutine(CountDown());
            QTEText.SetActive(true);
            if (keyToPress == 1)
            {
                qKey.SetActive(true);

                if (Input.anyKeyDown)
                {
                   QTEText.SetActive(false);
                     
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        correctKey = 1;
                        firstTime = false;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctKey = 2;
                        firstTime = false;
                        StartCoroutine(KeyPressing());
                    }
                }
            }

            if (keyToPress == 2)
            {
                wKey.SetActive(true);

                if (Input.anyKeyDown)
                {
                    QTEText.SetActive(false);

                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        correctKey = 1; 
                        firstTime = false;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctKey = 2;
                        firstTime = false;
                        StartCoroutine(KeyPressing());
                    }
                }
            }

            if (keyToPress == 3)
            {
                eKey.SetActive(true);
               
                if (Input.anyKeyDown)
                {
                    QTEText.SetActive(false);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        correctKey = 1;
                        firstTime = false;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctKey = 2;
                        firstTime = false;
                        StartCoroutine(KeyPressing());
                    }
                }
            }



        }

    }

    IEnumerator KeyPressing()
    {
        keyToPress = 4;

        if (correctKey == 1)
        {
            qKey.GetComponent<SpriteRenderer>().color = Color.green;
            wKey.GetComponent<SpriteRenderer>().color = Color.green;
            eKey.GetComponent<SpriteRenderer>().color = Color.green;
            countingDown = 2;
            yield return new WaitForSecondsRealtime(0.5f);
            qKey.SetActive(false);
            wKey.SetActive(false);
            eKey.SetActive(false);
            correctKey = 0;
            Time.timeScale = 1f;
        }


        if (correctKey == 2)
        {
            qKey.GetComponent<SpriteRenderer>().color = Color.red;
            wKey.GetComponent<SpriteRenderer>().color = Color.red;
            eKey.GetComponent<SpriteRenderer>().color = Color.red;
            countingDown = 2;
            yield return new WaitForSecondsRealtime(0.5f);
            qKey.SetActive(false);
            wKey.SetActive(false);
            eKey.SetActive(false);
            correctKey = 0;
            Time.timeScale = 1f;

        }
    }


    IEnumerator CountDown()
    {
        yield return new WaitForSecondsRealtime(5);
        if(countingDown == 1)
        {
            firstTime = false;
            QTEText.SetActive(false);
            qKey.GetComponent<SpriteRenderer>().color = Color.red;
            wKey.GetComponent<SpriteRenderer>().color = Color.red;
            eKey.GetComponent<SpriteRenderer>().color = Color.red;
            keyToPress = 4;
            countingDown = 2;
            countingDown = 2;
            yield return new WaitForSecondsRealtime(0.5f);
            qKey.SetActive(false);
            wKey.SetActive(false);
            eKey.SetActive(false);
            Time.timeScale = 1f;

            correctKey = 0;

        }
    }
}
