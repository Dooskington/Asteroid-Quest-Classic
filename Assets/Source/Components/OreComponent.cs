using Assets.Source.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreComponent : MonoBehaviour
{
    public Ore Ore { get; set; }

    private void Start()
    {
        name = Ore.name;
        GetComponent<SpriteRenderer>().sprite = Ore.sprite;

        float maxForce = 5.0f;
        Vector2 randomForce = new Vector2(Random.Range(-maxForce, maxForce), Random.Range(-maxForce, maxForce));
        GetComponent<Rigidbody2D>().AddForce(randomForce, ForceMode2D.Impulse);

        float minScale = 1.0f;
        float maxScale = 2.0f;
        float rand = Random.Range(minScale, maxScale);
        Vector3 randomScale = new Vector3(rand, rand, 1.0f);
        transform.localScale = randomScale;
    }
}
