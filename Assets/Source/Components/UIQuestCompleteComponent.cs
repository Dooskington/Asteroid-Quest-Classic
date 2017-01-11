using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestCompleteComponent : MonoBehaviour
{
    public float lengthTime = 1.5f;
    public Text rewardText;

    private float openTime;
    private bool isClosing;

    private Animator animator;

    public void Open(int rewardAmount)
    {
        isClosing = false;
        openTime = Time.time;
        rewardText.text = rewardAmount.ToString();

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
