using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject fadePanel;
    public UIPauseMenu pausePanel;

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
            pausePanel.State = UIPauseMenu.MenuState.Main;
            pausePanel.gameObject.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
            Cursor.visible = isPaused;
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
