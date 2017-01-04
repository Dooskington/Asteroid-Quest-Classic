using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretComponent : MonoBehaviour
{
    public bool isActive = false;
    public Vector2 targetPosition = Vector2.zero;
    public GameObject projectileSpawnObject;
    public GameObject projectilePrefab;
    public AudioEvent fireAudioEvent;

    public Texture2D defaultCursorSprite;
    public Texture2D crosshairSprite;

    private Rigidbody2D rootRigidbodyComponent;
    private ShipReactorComponent m_shipReactorComponent;

    public void Fire()
    {
        fireAudioEvent.Play(transform.position);

        GameObject projectile = Instantiate(projectilePrefab, 
            projectileSpawnObject.transform.position, 
            projectileSpawnObject.transform.rotation) as GameObject;

        projectile.GetComponent<Rigidbody2D>().velocity = rootRigidbodyComponent.velocity;
        projectile.GetComponent<ProjectileComponent>().owner = gameObject;
    }

    private void Awake()
    {
        rootRigidbodyComponent = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        m_shipReactorComponent = transform.parent.gameObject.GetComponent<ShipReactorComponent>();
    }

    private void Update()
    {
        if (isActive)
        {
            m_shipReactorComponent.UsePower(10.0f);
            //Cursor.SetCursor(crosshairSprite, Vector2.zero, CursorMode.Auto);

            float angleRad = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
    }
}
