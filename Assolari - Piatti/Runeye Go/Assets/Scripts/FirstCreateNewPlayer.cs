using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets._3D4amb_LIB.Scripts.Utils;
using Assets._3D4amb_LIB;

/** This script is called everytime you return to the menu scene,
 *  if you delete all players you have to create a new player,
 *  if you have just deleted the actual player, but there are other player
 *  the last player saved is loaded.
 *  If you haven't just deleted a player, the preferences of the actual player are loaded
 **/

public class FirstCreateNewPlayer : MonoBehaviour
{

    PrefManager pm;
    PlayerID actualPlayer;
    public GameObject MainMenuPanel;
    public GameObject NewPlayerPanel;
    public bool notfirst;


    // Use this for initialization
    void Start()
    {
        pm = GameObject.Find("PrefManager").GetComponent<PrefManager>();
        actualPlayer = pm.LoadActualPlayer();

        //If there is at least a player saved, the preferences of the last player saved are loaded 
        if (actualPlayer != null && pm.actualPlayer == null)
        {
            pm.actualPlayer = actualPlayer;
            pm.actualPlayerSettings = pm.LoadPlayerSettings();
        }
        //When there isn't an actual player, the panel to create a new player is activated
        else if (actualPlayer == null)
        {
            MainMenuPanel.SetActive(false);
            NewPlayerPanel.SetActive(true);
        }
    }



}
