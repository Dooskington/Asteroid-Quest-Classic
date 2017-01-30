using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    public float minAttackDistance = 4.0f;
    public float maxAttackDistance = 6.0f;
    public float sightRange = 2.5f;
    public LayerMask sightMask;

    private bool isTurning = false;
    private bool isSightClear = false;
    private Quaternion correction;
    private PlayerControllerComponent player;
    private ShipMovementComponent shipMovement;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControllerComponent>();
        shipMovement = GetComponent<ShipMovementComponent>();
    }

    private void Update()
    {
        if (!player)
        {
            return;
        }

        Vector3 turnDirection = (player.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, turnDirection, sightRange, sightMask);
        if (hit.collider)
        {
            isSightClear = hit.collider.CompareTag("Player");
        }
        else
        {
            isSightClear = true;
        }

        Debug.DrawLine(transform.position, player.transform.position, isSightClear ? Color.green : Color.red);

        if (!isSightClear && !isTurning)
        {
            isTurning = true;

            float rand = Random.Range(0.0f, 1.0f);
            if (rand < 0.5f)
            {
                correction = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            }
            else
            {
                correction = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            }
            Debug.Log(rand);
        }

        turnDirection = correction * turnDirection;

        if (isSightClear)
        {
            correction = Quaternion.identity;
            isTurning = false;
        }

        Debug.DrawRay(transform.position, turnDirection * sightRange, Color.cyan);

        shipMovement.Turn(turnDirection);

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if ((distance > maxAttackDistance) || !isSightClear)
        {
            shipMovement.thrust = 1.0f;
        }
        else if (distance < minAttackDistance)
        {
            //shipMovement.thrust = -0.1f;
        }
        else
        {
            shipMovement.thrust = 0.0f;
        }
    }
}
