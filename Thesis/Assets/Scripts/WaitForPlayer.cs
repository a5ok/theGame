using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPlayer : MonoBehaviour {

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject start;
    [SerializeField] public GameObject text;
    private bool waitingToStartGame = true;

	// Use this for initialization
	void Start () {
        player.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
        if(waitingToStartGame && Input.GetKeyDown(KeyCode.Space))
        {
            waitingToStartGame = false;
            player.SetActive(true);
            start.SetActive(false);
            text.SetActive(false);
        }
        
    }
}
