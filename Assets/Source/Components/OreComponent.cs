using Assets.Source.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreComponent : MonoBehaviour
{
    public Ore Ore { get; set; }
    public float lifetimeMinutes = 2.5f;

    private void Start()
    {
        name = Ore.oreName;
        GetComponent<SpriteRenderer>().sprite = Ore.sprite;

        float maxForce = 2.5f;
        Vector2 randomForce = new Vector2(Random.Range(-maxForce, maxForce), Random.Range(-maxForce, maxForce));
        GetComponent<Rigidbody2D>().AddForce(randomForce, ForceMode2D.Impulse);

        float minScale = 1.0f;
        float maxScale = 2.0f;
        float rand = Random.Range(minScale, maxScale);
        transform.localScale = transform.localScale * rand;

        Destroy(gameObject, lifetimeMinutes * 60.0f);
    }
}
