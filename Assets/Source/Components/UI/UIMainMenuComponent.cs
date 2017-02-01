using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuComponent : MonoBehaviour
{
    public GameObject mainPanel;
    public UIOptionsMenuComponent optionsPanel;
    public CanvasGroup fader;
    public AudioSource audioSource;
    public bool isLoading = false;

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
        Cursor.visible = true;
        optionsPanel.backAction += delegate { State = MenuState.Main; };
    }

    public void OnClickPlay()
    {
        if (isLoading)
        {
            return;
        }

        isLoading = true;

        Juice.Instance.FadeVolume(audioSource, 0.75f, 0.0f);
        Juice.Instance.FadeGroup(fader, 1.0f, 1.0f, false, () => { isLoading = false; SceneManager.LoadScene("Game"); });
    }

    public void OnClickOptions()
    {
        if (isLoading)
        {
            return;
        }

        State = MenuState.Options;
    }

    public void Back()
    {
        if (isLoading)
        {
            return;
        }

        State = MenuState.Main;
    }

    public void OnClickQuit()
    {
        if (isLoading)
        {
            return;
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
