using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESystem : MonoBehaviour {

    [SerializeField] GameObject slider;
    private bool triggered;
    private bool firstTime;
    public LayerMask whatIsPlayer;
    private Collider2D myCollider;

    // Use this for initialization
    void Start () {

        firstTime = true;
        myCollider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

        triggered = Physics2D.IsTouchingLayers(myCollider, whatIsPlayer);

        if(triggered && firstTime)
        {
            firstTime = false;
            Time.timeScale = 0f;
            StartCoroutine(QTE());
        }
    }

    public IEnumerator QTE()
    {
        slider.SetActive(true);





        yield return null;
    }
}
