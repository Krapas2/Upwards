using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerPowerupBomb : MonoBehaviour
{

    public int bombNumber;
    public BombController bomb;
    public float throwSpeed;
    public float distanceOffsetFactor;

    public Tilemap ground;

    private int counter;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        counter = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("Bomb") && counter < bombNumber)
        {
            ThrowBomb();
            counter++;
        }
    }

    void ThrowBomb()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float yOffset = Mathf.Pow(Vector2.Distance(transform.position, mousePos), 2) / distanceOffsetFactor;

        BombController curBomb = Instantiate(bomb, transform.position, Quaternion.identity);
        curBomb.ground = ground;
        curBomb.rb.velocity = ((mousePos - (Vector2)transform.position) + new Vector2(0, yOffset)).normalized * throwSpeed;
        curBomb.rb.angularVelocity = Random.Range(-360f,360f);
    }
}
