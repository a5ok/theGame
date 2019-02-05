using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this to a button that let the player
/// choose an avatar when it creates its PlayerID
/// </summary>
public class AvatarChooseButton : MonoBehaviour
{
    /// <summary>
    /// A reference to the PrefManager object
    /// </summary>
    public GameObject PrefManager;

    /// <summary>
    /// Id (index in the array) of the current avatar shown
    /// </summary>
    public int avatarId = 0;

    private Sprite[] avatars;

    void Start()
    {
        avatars = PrefManager.GetComponent<PrefManager>().allAvatars;
        avatarId = 0;
        gameObject.GetComponent<Image>().sprite = avatars[avatarId];
    }

    /// <summary>
    /// Show the next avatar
    /// </summary>
    public void OnClick()
    {
        avatarId = (avatarId + 1) % avatars.Length;
        gameObject.GetComponent<Image>().sprite = avatars[avatarId];
    }
}
