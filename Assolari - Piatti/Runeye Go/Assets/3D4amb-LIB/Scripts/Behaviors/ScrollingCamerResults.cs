using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This simple script attached to the camera in ShowResults scene
/// make it able to scroll horizontally to clearly watch all the data
/// in the Graph, with the Input horizontal
/// </summary>
public class ScrollingCamerResults : MonoBehaviour
{
    /// <summary>
    /// ScrollSpeed of the camera.
    /// </summary>
    public float scrollSpeed = 8f;

    void Update()
    {
        float trasl = Input.GetAxis("Horizontal") * scrollSpeed;
        trasl *= Time.deltaTime;
        transform.Translate(trasl, 0, 0);

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButton("Fire1") || Input.GetButton("Jump"))
        {
            SceneManager.LoadScene("MainMenu");
        }
	}
}
