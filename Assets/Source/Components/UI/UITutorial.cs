using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    public CanvasGroup[] prompts;
    public float promptTime = 2.5f;
    public float fadeTime = 1.0f;
    public float delay = 2.0f;

    private int currentPromptIndex = 0;

    public void Open()
    {
        if (prompts.Length == 0)
        {
            return;
        }

        gameObject.SetActive(true);
        Invoke("FadeToNext", delay);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void FadeToNext()
    {
        if ((currentPromptIndex) == prompts.Length)
        {
            CanvasGroup finalPrompt = prompts[currentPromptIndex - 1];
            FadeOut(finalPrompt);
        }

        if (prompts.Length <= currentPromptIndex)
        {
            return;
        }

        CanvasGroup prompt = prompts[currentPromptIndex];
        if (prompt == null)
        {
            return;
        }

        if (currentPromptIndex > 0)
        {
            CanvasGroup fadeOut = prompts[currentPromptIndex - 1];
            FadeOut(fadeOut);
        }

        Juice.Instance.FadeGroup(prompt, fadeTime, 1.0f, true, () => { Invoke("FadeToNext", promptTime); });
        currentPromptIndex++;
    }

    private void FadeOut(CanvasGroup prompt)
    {
        Juice.Instance.FadeGroup(prompt, fadeTime / 2.0f, 0.0f, true, () => { Destroy(prompt.gameObject); });
    }
}
