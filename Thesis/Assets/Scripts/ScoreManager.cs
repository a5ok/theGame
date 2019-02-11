using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        score = GameObject.Find("SessionManager").GetComponent<SessionManager>().GetScore();
        this.GetComponent<TextMesh>().text = "Score: " + score.ToString();
    }
}
