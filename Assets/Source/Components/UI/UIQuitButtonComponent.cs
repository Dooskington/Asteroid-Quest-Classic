using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuitButtonComponent : MonoBehaviour
{
    public void OnQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
