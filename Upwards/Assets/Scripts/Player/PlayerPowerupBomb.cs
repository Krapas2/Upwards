using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerPowerupBomb : MonoBehaviour
{

    public BombController bomb;
    public float throwSpeed;
    public float distanceOffsetFactor;

    public Tilemap ground;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

    }

    void Update()
    {
        if (Input.GetButtonDown("Bomb"))
        {
            ThrowRope();
        }
    }

    void ThrowRope()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float yOffset = Mathf.Pow(Vector2.Distance(transform.position, mousePos), 2) / distanceOffsetFactor;

        BombController curBomb = Instantiate(bomb, transform.position, Quaternion.identity);
        curBomb.ground = ground;
        curBomb.rb.velocity = ((mousePos - (Vector2)transform.position) + new Vector2(0, yOffset)).normalized * throwSpeed;
    }
}
