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

        keyToPress = Random.Range(1, 3);
        firstTime = true;
        myCollider = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {

        triggered = Physics2D.IsTouchingLayers(myCollider, whatIsPlayer);

        if (triggered && firstTime)
        {
            firstTime = false;
            Time.timeScale = 0f;
            countingDown = 1;
            StartCoroutine(CountDown());
            QTEText.GetComponent<Text>().text = "Premi rapidamente il tasto mostrato ";

            if (keyToPress == 1)
            {
                qKey.SetActive(true);
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        correctKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctKey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }

            if (keyToPress == 2)
            {
                wKey.SetActive(true);
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        correctKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctKey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }

            if (keyToPress == 3)
            {
                eKey.SetActive(true);
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        correctKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        correctKey = 2;
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
            countingDown = 2;
            QTEText.GetComponent<Text>().text = "BRAVO!!";
            yield return new WaitForSeconds(1.5f);
            correctKey = 0;
            QTEText.GetComponent<Text>().text = "";


        }

        if (correctKey == 2)
        {
            countingDown = 2;
            QTEText.GetComponent<Text>().text = "NOOOOOOOOO";
            yield return new WaitForSeconds(1.5f);
            correctKey = 0;
            QTEText.GetComponent<Text>().text = "";

        }
    }


    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(5f);

        if(countingDown == 1)
        {
            keyToPress = 4;
            countingDown = 2;
            countingDown = 2;
            QTEText.GetComponent<Text>().text = "NOOOOOOOOO";
            yield return new WaitForSeconds(1.5f);
            correctKey = 0;
            QTEText.GetComponent<Text>().text = "";
        }
    }
}
