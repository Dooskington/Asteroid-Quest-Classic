using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeaponComponent : MonoBehaviour
{
    public Transform projectileSpawn;
    public GameObject projectilePrefab;
    public AudioEvent fireAudioEvent;
    public float fireFrequency = 0.75f;

    private float lastFireTime;
    private Collider2D colliderComponent;
    private ShipReactorComponent shipReactorComponent;

    public void Fire()
    {
        if ((Time.time - lastFireTime) < fireFrequency)
        {
           return;
        }

        fireAudioEvent.Play(transform.position);

        GameObject projectile = Instantiate(projectilePrefab,
            projectileSpawn.position,
            projectileSpawn.rotation) as GameObject;

        projectile.GetComponent<ProjectileComponent>().owner = gameObject;
        Physics2D.IgnoreCollision(colliderComponent, projectile.GetComponent<Collider2D>());

        lastFireTime = Time.time;
        shipReactorComponent.UsePower(100.0f);
    }

    private void Awake()
    {
        colliderComponent = GetComponent<Collider2D>();
        shipReactorComponent = GetComponent<ShipReactorComponent>();
    }
}
