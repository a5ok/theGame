using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool paused = false;
    public GameObject pauseTextSx;
    public GameObject pauseTextDx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !paused)
            paused = true;

        else if (paused)
        {
            Time.timeScale = 0f;
            pauseText.SetActive(true);  
            if (Input.GetButtonDown("Fire2"))
            {
                Time.timeScale = 1f;
                paused = false;
                pauseText.SetActive(false);
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
