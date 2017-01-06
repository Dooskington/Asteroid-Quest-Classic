using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipTargetingComponent : MonoBehaviour
{
    public Transform target;
    public Transform lockedTarget;
    public UITargetLockComponent targetLockPanel;
    public UITargetComponent targetPanel;
    public UIReticleComponent reticle;

    public void Target(Transform target)
    {
        if (!target)
        {
            ClearTarget();
            return;
        }

        this.target = target;
        reticle.Target = target;
        targetPanel.Open(target);

        if ((reticle.Target == target) && reticle.isLocked)
        {
            return;
        }

        reticle.isLocked = false;
    }

    public void ClearTarget()
    {
        target = null;
        targetPanel.Close();

        if (!reticle.isLocked)
        {
            reticle.Target = null;
        }
    }

    public void LockTarget()
    {
        lockedTarget = target;
        reticle.isLocked = true;
        targetLockPanel.Open(reticle.Target);
    }

    public void UnlockTarget()
    {
        lockedTarget = null;
        reticle.isLocked = false;
        targetLockPanel.Close();
    }
}
