using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPlayer : MonoBehaviour {

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject sword;
    [SerializeField] public GameObject start;
    [SerializeField] public GameObject textSx;
    [SerializeField] public GameObject textDx;
    private GameObject[] enemies;
    private bool waitingToStartGame = true;

	// Use this for initialization
	void Start () {
        player.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("EnemyTag");

        foreach (GameObject gos in enemies)
        {
            gos.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
        if(waitingToStartGame && Input.GetButtonDown("Jump"))
        {
            waitingToStartGame = false;
            player.SetActive(true);
            sword.SetActive(false);
            start.SetActive(false);
            textSx.SetActive(false);
            textDx.SetActive(false);

            foreach (GameObject gos in enemies)
            {
                gos.SetActive(true);
            }
        }
        
    }
}
