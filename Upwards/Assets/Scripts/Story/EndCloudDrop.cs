using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EndCloudDrop : MonoBehaviour
{
    public float speed;
	public float acceleration = 1f;
	public float rotateRate = .125f;
    public Color[] colors;
    public LayerMask playerLayer;

    private Transform player;
	private Rigidbody2D rb;

    void Start()
    {
		rb = GetComponent<Rigidbody2D> ();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];

        transform.up = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0).normalized;
        rb.velocity = transform.up * speed / 2f;
		transform.localScale *= Random.Range(.125f,.875f);
    }

    void Update()
    {
		Movement();
		Rotation();

        if(Physics2D.OverlapCircle(transform.position, .25f, playerLayer)){
            FindObjectOfType<EndCloudManager>().particleAmount--;
			Destroy(gameObject);
        }
    }

	void Movement()
	{
		Vector2 desiredVelocity = transform.up * speed;
		Vector3 lerpedVelocity = Vector3.Lerp(rb.velocity, desiredVelocity, acceleration * Time.deltaTime);
		rb.velocity = lerpedVelocity;
	}
	void Rotation()
	{
		Vector2 desiredUp = player.position - transform.position;
		Vector3 lerpedUp = Vector3.Lerp(transform.up, desiredUp, rotateRate * Time.deltaTime);
		transform.up = lerpedUp;
	}
}
