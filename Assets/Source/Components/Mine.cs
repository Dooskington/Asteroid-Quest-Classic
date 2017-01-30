using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float baseDamage = 50.0f;
    public float radius = 2.5f;
    public float explodeForce = 100.0f;
    public GameObject explosionPrefab;
    public LayerMask triggerLayerMask;
    public LayerMask sightLayerMask;
    public GameObject blinker;
    public float blinkFrequency = 1.0f;
    public AudioEvent blinkAudio;

    private Rigidbody2D rb;
    private float lastBlinkTime;
    private bool isTriggered;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, triggerLayerMask);
        if (collider)
        {
            if (!isTriggered)
            {
                isTriggered = true;
            }

            Vector3 direction = (collider.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, sightLayerMask);
            if (hit.collider)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    rb.AddForce(direction * 20.0f);
                }
            }

            Debug.DrawRay(transform.position, direction);
        }
        else
        {
            isTriggered = false;
        }

        if (isTriggered)
        {
            if ((Time.time - lastBlinkTime) >= blinkFrequency)
            {
                Blink();
            }
        }
        else
        {
            blinker.SetActive(false);
        }
    }

    private void Blink()
    {
        lastBlinkTime = Time.time;
        blinker.SetActive(!blinker.activeSelf);

        if (blinker.activeSelf)
        {
            blinkAudio.Play(transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ShipDefenseComponent shipDefense = collision.gameObject.GetComponent<ShipDefenseComponent>();
        if (shipDefense)
        {
            shipDefense.hull -= baseDamage;
        }

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb)
        {
            Vector3 dir = (transform.position - collision.transform.position).normalized;
            rb.AddForceAtPosition(-dir * explodeForce, collision.transform.position, ForceMode2D.Impulse);
            rb.AddTorque(explodeForce / 20.0f, ForceMode2D.Impulse);
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
