using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button optionsButton;
    public Button menuButton;
    public Button quitButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => { Resume(); });
        optionsButton.interactable = false;
        menuButton.onClick.AddListener(() => { SceneManager.LoadScene("Menu"); });
        quitButton.onClick.AddListener(() => { Quit(); });
    }

    private void Resume()
    {
        FindObjectOfType<PauseManager>().IsPaused = false;
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
