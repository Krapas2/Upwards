using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPrompt : MonoBehaviour
{
    // Handles the movement of this prompt

    private Vector3 origPos;
    private Transform playerPos;

    void Start()
    {
        origPos = transform.position;
        playerPos = FindObjectOfType<PlayerMovement>().transform;
    }

    void OnEnable() {
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(origPos, playerPos.position,.125f);
    }
}
