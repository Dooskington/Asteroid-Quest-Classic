using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITargetLockComponent : MonoBehaviour
{
    public Text nameText;
    public Image shipImage;

    private Transform target;
    private PlayerControllerComponent playerComponent;

    public void Open(Transform target)
    {
        if (!target)
        {
            return;
        }

        this.target = target;
        nameText.text = target.name;

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnUnlockClick()
    {
        playerComponent.UnlockTarget();
        playerComponent.ClearTarget();
    }

    public void OnClick()
    {
        playerComponent.Target(target);
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        playerComponent = FindObjectOfType<PlayerControllerComponent>();
    }
}
