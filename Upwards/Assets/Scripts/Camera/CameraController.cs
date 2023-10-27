using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public GameObject target;
	public float speedOffsetMultiplier = .125f;
	public float smoothMoveSpeed = .25f;


	public Vector3 offset;
	private Rigidbody2D PlayerRB;
	private Camera cam;
	private float origSize;

	// Use this for initialization
	// Use this for initialization
	void Start ()
	{
		//offset = transform.position - target.transform.position;
		PlayerRB = target.GetComponent<Rigidbody2D> ();
		cam = GetComponent<Camera> ();
		origSize = GetComponent<Camera> ().orthographicSize;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//follow target
		transform.position = target.transform.position + offset;
/*
		Vector3 speedOffset = new Vector3 (
			PlayerRB.velocity.x * speedOffsetMultiplier,
			PlayerRB.velocity.y * speedOffsetMultiplier,
			-10
		);
		Vector3 desiredPosition = target.transform.position + offset + speedOffset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothMoveSpeed);
		transform.position = smoothedPosition;

		//snap to target
		if(Vector3.Distance(transform.position, desiredPosition) < .05)
			transform.position = desiredPosition;
*/
	}
}
