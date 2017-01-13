using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlayButtonComponent : MonoBehaviour
{
    public void OnPlayClick()
    {
        SceneManager.LoadScene("Main");
    }
}
