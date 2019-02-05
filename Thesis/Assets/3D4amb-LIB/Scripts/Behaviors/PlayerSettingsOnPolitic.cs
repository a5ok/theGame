using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manage the opening of the right panel in the MainMenu
/// </summary>
public class PlayerSettingsOnPolitic : MonoBehaviour
{
    /// <summary>
    /// <see cref="PrefManager"/>
    /// </summary>
    public GameObject PrefManager;
    /// <summary>
    /// The Panel that must be openend if the <see cref="GamePolicy.DifficultyBounds.FROM_FILE"/>
    /// </summary>
    public GameObject Panel_ByFile;
    /// <summary>
    /// The Panel that must be openend if the <see cref="GamePolicy.DifficultyBounds.FROM_PLAYER"/>
    /// </summary>
    public GameObject Panel_ByPlayer;

    private GamePolicy.DifficultyBounds PolicyDifficultyBounds;

	void Start ()
    {
        PolicyDifficultyBounds = PrefManager.GetComponent<PrefManager>().PolicyDifficultyBounds;
	}

    public void OpenNextPanel()
    {
        switch (PolicyDifficultyBounds)
        {
            case (GamePolicy.DifficultyBounds.FROM_FILE):
                Panel_ByFile.SetActive(true);
                break;
            case (GamePolicy.DifficultyBounds.FROM_PLAYER):
                Panel_ByPlayer.SetActive(true);
                break;
        }
    }
}
