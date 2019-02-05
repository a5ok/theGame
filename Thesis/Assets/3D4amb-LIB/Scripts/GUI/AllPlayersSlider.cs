using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this script to a slider that let you select
/// a Player from allPlayers loaded
/// </summary>
public class AllPlayersSlider : MonoBehaviour
{
    /// <summary>
    /// Reference to the PrefManager
    /// </summary>
    public GameObject PrefManager;
    /// <summary>
    /// Avatar of the actually selected Player
    /// </summary>
    public GameObject PlayerAvatar;
    /// <summary>
    /// Name of the actually selected Player
    /// </summary>
    public GameObject PlayerName;

    private int playerIndex;
    private PlayerID[] allPlayers;

    void OnEnable ()
    {
        allPlayers = PrefManager.GetComponent<PrefManager>().LoadAllPlayers();
        //Debug.Log("allPlayers " + (allPlayers!=null));
        for (int i=0;i<allPlayers.Length;i++)
        {
            if (allPlayers[i].Equals(PrefManager.GetComponent<PrefManager>().actualPlayer))
            {
                playerIndex = i;
                break;
            }
        }
        gameObject.GetComponent<Slider>().value = playerIndex;
        gameObject.GetComponent<Slider>().maxValue = allPlayers.Length-1;
        UpdatePlayerData();
    }

    /// <summary>
    /// Called when the slider change position, update the data shown
    /// </summary>
    public void UpdatePlayerData()
    {
        PrefManager.GetComponent<PrefManager>().actualPlayer = allPlayers[(int)gameObject.GetComponent<Slider>().value];
        PlayerAvatar.GetComponent<Image>().sprite = PrefManager.GetComponent<PrefManager>().allAvatars[PrefManager.GetComponent<PrefManager>().actualPlayer.IdAvatar];
        PlayerName.GetComponent<Text>().text = PrefManager.GetComponent<PrefManager>().actualPlayer.PlayerName;
    }
}
