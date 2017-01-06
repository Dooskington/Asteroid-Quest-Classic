using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretComponent : MonoBehaviour
{
    public bool isActive = false;
    public GameObject projectileSpawnObject;
    public GameObject projectilePrefab;
    public AudioEvent fireAudioEvent;
    public Texture2D defaultCursorSprite;
    public Texture2D crosshairSprite;
    public float baseFireFrequency = 1.0f;

    private float lastFireTime;
    private float fireFrequency;
    private Rigidbody2D rootRigidbodyComponent;
    private ShipReactorComponent shipReactorComponent;
    private ShipTargetingComponent shipTargetingComponent;

    private void Awake()
    {
        rootRigidbodyComponent = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        shipReactorComponent = transform.parent.gameObject.GetComponent<ShipReactorComponent>();
        shipTargetingComponent = transform.parent.gameObject.GetComponent<ShipTargetingComponent>();
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        shipReactorComponent.UsePower(10.0f);

        Transform target = shipTargetingComponent.lockedTarget;
        if (target)
        {
            float angleRad = Mathf.Atan2(
                target.position.y - transform.position.y,
                target.position.x - transform.position.x);

            float angleDeg = (180.0f / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.Euler(0, 0, angleDeg);

            if ((Time.time - lastFireTime) >= fireFrequency)
            {
                Fire();
                lastFireTime = Time.time;
                fireFrequency = baseFireFrequency + Random.Range(-0.25f, 0.5f);
            }
        }
    }

    private void Fire()
    {
        fireAudioEvent.Play(transform.position);

        GameObject projectile = Instantiate(projectilePrefab,
            projectileSpawnObject.transform.position,
            projectileSpawnObject.transform.rotation) as GameObject;

        projectile.GetComponent<Rigidbody2D>().velocity = rootRigidbodyComponent.velocity;
        projectile.GetComponent<ProjectileComponent>().owner = gameObject;
    }
}
