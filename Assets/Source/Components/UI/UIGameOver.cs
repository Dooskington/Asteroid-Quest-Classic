using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public Text scoreText;
    public Text reasonText;
    public Button quitButton;
    public bool isOpen = false;
    public float fadeTime = 1.0f;

    public void Open(string reason, int score)
    {
        reasonText.text = reason;
        scoreText.text = "Net worth = " + score.ToString() + " credits";

        gameObject.SetActive(true);
        isOpen = true;
        Juice.Instance.FadeGroup(GetComponent<CanvasGroup>(), fadeTime, 1.0f);

        Cursor.visible = true;
    }

    public void Close()
    {
        Cursor.visible = false;

        isOpen = false;
        Juice.Instance.FadeGroup(GetComponent<CanvasGroup>(), fadeTime, 0.0f, true, () => { gameObject.SetActive(false); });
    }

    private void Awake()
    {
        quitButton.onClick.AddListener(() => { SceneManager.LoadScene("Menu"); });
    }
}
