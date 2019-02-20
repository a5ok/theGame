using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPlayer : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject sword;
    [SerializeField] public GameObject start;
    [SerializeField] public GameObject textSx;
    [SerializeField] public GameObject textDx;
    private bool waitingToStartGame = true;
    public static bool waitForPlayer;

    // Use this for initialization
    void Start()
    {

        player.SetActive(false);
        waitForPlayer = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (waitingToStartGame && Input.GetButtonDown("Jump"))
        {
            waitingToStartGame = false;
            waitForPlayer = false;
            player.SetActive(true);
            sword.SetActive(false);
            start.SetActive(false);
            textSx.SetActive(false);
            textDx.SetActive(false);

        }
    }


} 
