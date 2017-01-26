using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject pausePanel;

    private bool isPaused;
    public bool IsPaused
    {
        get
        {
            return isPaused;
        }
        
        set
        {
            isPaused = value;
            fadePanel.SetActive(isPaused);
            pausePanel.SetActive(isPaused);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
        }
    }
}
