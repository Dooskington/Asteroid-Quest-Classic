using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteComponent : MonoBehaviour
{
    public Sprite[] sprites;
    public bool addCollider = true;

    private void Awake()
    {
        if (sprites.Length == 0)
        {
            return;
        }

        Sprite sprite = sprites[Random.Range(0, sprites.Length)];
        GetComponent<SpriteRenderer>().sprite = sprite;

        if (addCollider)
        {
            gameObject.AddComponent<CircleCollider2D>();
        }
    }

}
