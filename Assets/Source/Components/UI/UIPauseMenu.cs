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
    public UIOptionsMenuComponent optionsPanel;

    public enum MenuState { Main, Options };
    private MenuState state = MenuState.Main;
    public MenuState State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;

            gameObject.SetActive(state == MenuState.Main);
            optionsPanel.gameObject.SetActive(state == MenuState.Options);
        }
    }

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => { Resume(); });
        optionsButton.onClick.AddListener(() => { State = MenuState.Options; });
        menuButton.onClick.AddListener(() => { SceneManager.LoadScene("Menu"); });
        quitButton.onClick.AddListener(() => { Quit(); });

        optionsPanel.backAction += delegate { State = MenuState.Main; };
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
