using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    public Button closeButton;

    public void Open()
    {
        closeButton.onClick.AddListener(() => { Close(); });
        Cursor.visible = true;
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Close()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
