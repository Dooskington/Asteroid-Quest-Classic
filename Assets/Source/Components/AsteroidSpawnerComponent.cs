using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerComponent : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int maxAsteroids = 20;
    public Bounds spawnBounds;

    private List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        Spawn(maxAsteroids);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + spawnBounds.center, spawnBounds.size);
    }

    private void Spawn(int amount = 1)
    {
        for(int i = 0; i < amount; i++)
        {
            Vector3 position = transform.position + spawnBounds.center + new Vector3(
                Random.Range(-spawnBounds.extents.x, spawnBounds.extents.x),
                Random.Range(-spawnBounds.extents.y, spawnBounds.extents.y),
                Random.Range(-spawnBounds.extents.z, spawnBounds.extents.z));

            RaycastHit2D hit = Physics2D.CircleCast(position, 1.0f, Vector2.zero);
            if (hit.collider)
            {
                continue;
            }

            GameObject asteroid = Instantiate(asteroidPrefab, position, Quaternion.identity, transform) as GameObject;
            spawned.Add(asteroid);
        }
    }
}
