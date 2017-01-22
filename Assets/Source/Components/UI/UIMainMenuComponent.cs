using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuComponent : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionsPanel;

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

            mainPanel.SetActive(state == MenuState.Main);
            optionsPanel.SetActive(state == MenuState.Options);
        }
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickOptions()
    {
        State = MenuState.Options;
    }

    public void Back()
    {
        State = MenuState.Main;
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
