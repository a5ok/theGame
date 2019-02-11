using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPlayer : MonoBehaviour {

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject start;
    [SerializeField] public GameObject textSx;
    [SerializeField] public GameObject textDx;
    private bool waitingToStartGame = true;

	// Use this for initialization
	void Start () {
        player.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
        if(waitingToStartGame && Input.GetButtonDown("Jump"))
        {
            waitingToStartGame = false;
            player.SetActive(true);
            start.SetActive(false);
            textSx.SetActive(false);
            textDx.SetActive(false);
        }
        
    }
}
