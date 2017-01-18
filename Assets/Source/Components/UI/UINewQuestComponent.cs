using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINewQuestComponent : MonoBehaviour
{
    public AudioEvent openAudio;
    public float lengthTime = 1.5f;
    public Text questText;

    private float openTime;
    private bool isClosing;

    private Animator animator;

    public void Open(Quest quest)
    {
        openAudio.Play(transform.position);

        isClosing = false;
        openTime = Time.time;
        questText.text = quest.endStation.stationName;

        animator.SetBool("isOpen", true);
    }

    public void Close()
    {
        isClosing = true;

        animator.SetBool("isOpen", false);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isClosing)
        {
            return;
        }

        if ((Time.time - openTime) >= lengthTime)
        {
            Close();
        }
    }
}
