using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool paused;
    public GameObject pauseTextSx;
    public GameObject pauseTextDx;
    public GameObject textBackGroundSx;
    public GameObject textBackGroundDx;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !paused && !WaitForPlayer.waitForPlayer && !EndLevel.endLevel && !Death.isDead)
            paused = true;

        else if (paused)
        {
            Time.timeScale = 0f;
            pauseTextSx.SetActive(true);
            pauseTextDx.SetActive(true);
            textBackGroundSx.SetActive(true);
            textBackGroundDx.SetActive(true);

            if (Input.GetButtonDown("Fire2"))
            {
                Time.timeScale = 1f;
                paused = false;
                pauseTextSx.SetActive(false);
                pauseTextDx.SetActive(false);
                textBackGroundSx.SetActive(false);
                textBackGroundDx.SetActive(false);
            }
            if (Input.GetButtonDown("Jump") && paused)
            {
                Time.timeScale = 1f;
                paused = false;
                SceneManager.LoadScene("MainMenu");
            }
        }

       

    }
}
