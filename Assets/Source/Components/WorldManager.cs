using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public float chunkOffset = 25.0f;
    public GameObject chunkPrefab;
    public Vector2 currentChunk;

    private GameObject Player { get; set; }

    private Dictionary<Vector2, GameObject> Chunks { get; set; }

    public void Regenerate()
    {
        GameObject baseChunk = null;
        Chunks.TryGetValue(Vector2.zero, out baseChunk);

        foreach (var chunk in Chunks)
        {
            Vector2 location = chunk.Key;
            GameObject chunkObject = chunk.Value;

            if (location != Vector2.zero)
            {
                Destroy(chunkObject);
            }
        }

        Chunks.Clear();
        Chunks.Add(Vector2.zero, baseChunk);

        GenerateChunk(new Vector2(0, 0), true);
    }

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Chunks = new Dictionary<Vector2, GameObject>();

        GenerateChunk(new Vector2(0, 0), true);
    }

    private void Update()
    {
        if (Player == null)
        {
            return;
        }

        if (Player.transform.position.x >= 0)
        {
            currentChunk.x = (int) ((Player.transform.position.x + (chunkOffset / 2.0f)) / chunkOffset);
        }
        else
        {
            currentChunk.x = (int) ((Player.transform.position.x - (chunkOffset / 2.0f)) / chunkOffset);
        }

        if (Player.transform.position.y >= 0)
        {
            currentChunk.y = (int) ((Player.transform.position.y + (chunkOffset / 2.0f)) / chunkOffset);
        }
        else
        {
            currentChunk.y = (int) ((Player.transform.position.y - (chunkOffset / 2.0f)) / chunkOffset);
        }

        GenerateChunk(currentChunk, true);

        foreach(var chunk in Chunks)
        {
            if (!IsChunkVisible(chunk.Key))
            {
                if (chunk.Value.activeSelf)
                {
                    chunk.Value.SetActive(false);
                }
            }
            else
            {
                if (!chunk.Value.activeSelf)
                {
                    chunk.Value.SetActive(true);
                }
            }
        }
    }

    private void GenerateChunk(Vector2 location, bool generateSurrounding = false)
    {
        Vector2 position = location * chunkOffset;
        if (Chunks.ContainsKey(location) && !generateSurrounding)
        {
            return;
        }

        if (generateSurrounding)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    GenerateChunk(location + new Vector2(x, y));
                }
            }
        }
        else
        {
            Chunks[location] = Instantiate(chunkPrefab, position, Quaternion.identity, transform) as GameObject;
        }
    }

    private bool IsChunkVisible(Vector2 coordinates)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (coordinates == (currentChunk + new Vector2(x, y)))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
