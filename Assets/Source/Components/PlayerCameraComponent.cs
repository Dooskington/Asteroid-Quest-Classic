using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraComponent : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        if (player)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

}
