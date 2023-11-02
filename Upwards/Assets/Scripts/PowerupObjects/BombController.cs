using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{

    public float explosionRadius;
    public float waitTime;
    public LayerMask itemSources;
    public GameObject particles;
    public Tilemap ground;

    private int intExplosionRadius;

    [HideInInspector]
    public Rigidbody2D rb;

    private AudioManager _audManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _audManager = FindObjectOfType<AudioManager>();
        intExplosionRadius = (int)explosionRadius;
        Invoke("Explode", waitTime);
    }
    void Explode()
    {
        foreach (Collider2D itemSource in Physics2D.OverlapCircleAll(transform.position, explosionRadius, itemSources))
        {
            itemSource.GetComponent<ItemSource>().Drop();
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
        //PARTICLES
        Instantiate(particles, transform.position, Quaternion.identity);
        if (_audManager) _audManager.Play("Boom");
        Destroy(gameObject);
    }
}
