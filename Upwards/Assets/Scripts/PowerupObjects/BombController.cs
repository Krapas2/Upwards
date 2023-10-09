using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{

    public float explosionRadius;
    public float waitTime;
    public LayerMask itemSources;

    public Tilemap ground;

    private int intExplosionRadius;

    [HideInInspector]
    public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        intExplosionRadius = (int)explosionRadius;
        Invoke("Explode", waitTime);
    }

    void Explode()
    {
        foreach (Collider2D itemSource in Physics2D.OverlapCircleAll(transform.position, explosionRadius, itemSources))
        {
            Destroy(itemSource.gameObject);
        }

        for (int y = -intExplosionRadius; y <= intExplosionRadius; y++)
        {
            for (int x = -intExplosionRadius; x <= intExplosionRadius; x++)
            {
                Vector3Int relativePosOfCurNeighbor = new Vector3Int(x, y, 0);
                if (Vector3Int.Distance(Vector3Int.zero, relativePosOfCurNeighbor) < explosionRadius)
                {
                    Vector3Int truePosOfCurNeighbor = relativePosOfCurNeighbor + Vector3Int.FloorToInt(transform.position);
                    ground.SetTile(truePosOfCurNeighbor, null);
                }
                ground.SetTile(Vector3Int.FloorToInt(transform.position), null);
            }
        }
        Destroy(gameObject);
    }
}
