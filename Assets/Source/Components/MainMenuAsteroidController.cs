using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAsteroidController : MonoBehaviour
{
    public GameObject asteroidsPrefab;
    public float spawnFrequency;

    private float lastSpawn;

    private void Awake()
    {
        Spawn();
    }

    private void Update()
    {
        if ((Time.time - lastSpawn) >= spawnFrequency)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(asteroidsPrefab, transform.position, Quaternion.identity);
        lastSpawn = Time.time;
    }
}
