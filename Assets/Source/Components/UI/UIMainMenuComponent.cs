using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuComponent : MonoBehaviour
{
    public GameObject mainPanel;
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

            mainPanel.SetActive(state == MenuState.Main);
            optionsPanel.gameObject.SetActive(state == MenuState.Options);
        }
    }

    private void Awake()
    {
        optionsPanel.backAction += delegate { State = MenuState.Main; };
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
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
