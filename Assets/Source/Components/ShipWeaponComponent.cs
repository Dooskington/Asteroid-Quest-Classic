using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeaponComponent : MonoBehaviour
{
    public Transform projectileSpawn;
    public GameObject projectilePrefab;
    public AudioEvent fireAudioEvent;
    public float fireFrequency = 0.75f;
    public float damage = 1;

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

        GameObject projectileObject = Instantiate(projectilePrefab,
            projectileSpawn.position,
            projectileSpawn.rotation) as GameObject;

        ProjectileComponent projectile = projectileObject.GetComponent<ProjectileComponent>();

        projectile.owner = gameObject;
        projectile.damage = damage;

        Physics2D.IgnoreCollision(colliderComponent, projectileObject.GetComponent<Collider2D>());

        lastFireTime = Time.time;
        shipReactorComponent.UsePower(100.0f);
    }

    private void Awake()
    {
        colliderComponent = GetComponent<Collider2D>();
        shipReactorComponent = GetComponent<ShipReactorComponent>();
    }
}
