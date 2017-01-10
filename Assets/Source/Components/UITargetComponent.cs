using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITargetComponent : MonoBehaviour
{
    public Text m_nameText;
    public Image m_shipImage;

    private PlayerControllerComponent m_playerComponent;
    private Transform target;

    public void Open(Transform target)
    {
        this.target = target;
        m_nameText.text = target.name;

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnOrbitButtonClick()
    {
        
    }

    public void OnTargetButtonClick()
    {
        m_playerComponent.Target(target);
    }

    public void OnLockTargetButtonClick()
    {
        m_playerComponent.LockTarget();
    }

    private void Awake()
    {
        m_playerComponent = FindObjectOfType<PlayerControllerComponent>();
    }
}
