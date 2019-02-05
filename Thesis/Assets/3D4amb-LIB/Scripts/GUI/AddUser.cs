using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this script on a Canvas object to add a new user
/// reading from an InputField GameObject
/// </summary>
public class AddUser : MonoBehaviour
{
    /// <summary>
    /// Reference to the Prefmanager, where the player
    /// will be added
    /// </summary>
    public GameObject PrefManager;
    /// <summary>
    /// A GameObject with an InputField component
    /// </summary>
    public GameObject InputFieldGo;
    /// <summary>
    /// A GameObject with an AvatarChooseButton componenet
    /// </summary>
    public GameObject AvatarButton;

    /// <summary>
    /// Add a player to the PrefManager, reading the name
    /// from the inputfield and the avatar from the avatarbutton
    /// </summary>
	public void Add()
    {
        string name = InputFieldGo.GetComponent<InputField>().text;
        int avatar = AvatarButton.GetComponent<AvatarChooseButton>().avatarId;
        PrefManager.GetComponent<PrefManager>().AddNewPlayer(new PlayerID(name, avatar));
    }
}
