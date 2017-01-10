using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretComponent : MonoBehaviour
{
    public bool isActive = false;
    public Vector3 target;
    public Transform projectileSpawnSocket;
    public GameObject projectilePrefab;
    public AudioEvent fireAudioEvent;
    public float baseFireFrequency = 0.75f;
    public float rotationSpeed = 90.0f;
    public float powerUsage = 10.0f;

    private float lastFireTime;
    private float fireFrequency;
    private Rigidbody2D rootRigidbodyComponent;
    private ShipReactorComponent shipReactorComponent;

    public void Fire()
    {
        if ((!isActive) || ((Time.time - lastFireTime) < fireFrequency))
        {
           return;
        }

        fireAudioEvent.Play(transform.position);

        GameObject projectile = Instantiate(projectilePrefab,
            projectileSpawnSocket.position,
            projectileSpawnSocket.rotation) as GameObject;

        projectile.GetComponent<Rigidbody2D>().velocity = rootRigidbodyComponent.velocity;
        projectile.GetComponent<ProjectileComponent>().owner = gameObject;

        lastFireTime = Time.time;
        fireFrequency = baseFireFrequency + Random.Range(-0.25f, 0.0f);
    }

    private void Awake()
    {
        rootRigidbodyComponent = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        shipReactorComponent = transform.parent.gameObject.GetComponent<ShipReactorComponent>();
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        shipReactorComponent.UsePower(powerUsage);

        float angleRad = Mathf.Atan2( target.y - transform.position.y, target.x - transform.position.x);
        float angleDeg = (180.0f / Mathf.PI) * angleRad;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.Euler(0, 0, angleDeg),
            rotationSpeed * Time.deltaTime);
    }
}
