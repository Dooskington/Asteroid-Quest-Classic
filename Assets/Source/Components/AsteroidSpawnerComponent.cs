using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerComponent : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int maxAsteroids = 20;
    public GameObject minePrefab;
    public int maxMines = 6;
    public float mineChance = 0.25f;
    public Bounds spawnBounds;
    public float minDistanceFromCenter = 15.0f;

    private List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        if (transform.position == Vector3.zero)
        {
            mineChance = 0;
        }

        Spawn(maxAsteroids);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + spawnBounds.center, spawnBounds.size);
    }

    private void Spawn(int amount = 1)
    {
        if (minePrefab)
        {
            float spawnMines = Random.Range(0.0f, 1.0f);
            if (spawnMines <= mineChance)
            {
                int mineCount = Random.Range(1, maxMines + 1);
                for (int i = 0; i < mineCount; i++)
                {
                    Vector3 position = transform.position + spawnBounds.center + new Vector3(
                        Random.Range(-spawnBounds.extents.x, spawnBounds.extents.x),
                        Random.Range(-spawnBounds.extents.y, spawnBounds.extents.y),
                        Random.Range(-spawnBounds.extents.z, spawnBounds.extents.z));

                    RaycastHit2D hit = Physics2D.CircleCast(position, 2.5f, Vector2.zero);
                    if (hit.collider)
                    {
                        mineCount++;
                        continue;
                    }

                    Instantiate(minePrefab, position, Quaternion.identity, transform);
                }
            }
        }

        for (int i = 0; i < amount; i++)
        {
            Vector3 position = transform.position + spawnBounds.center + new Vector3(
                Random.Range(-spawnBounds.extents.x, spawnBounds.extents.x),
                Random.Range(-spawnBounds.extents.y, spawnBounds.extents.y),
                Random.Range(-spawnBounds.extents.z, spawnBounds.extents.z));

            RaycastHit2D hit = Physics2D.CircleCast(position, 1.0f, Vector2.zero);
            if (hit.collider || (Vector3.Distance(position, Vector3.zero) < minDistanceFromCenter))
            {
                continue;
            }

            GameObject asteroid = Instantiate(asteroidPrefab, position, Quaternion.identity, transform) as GameObject;
            spawned.Add(asteroid);
        }
    }
}
