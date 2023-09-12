using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeExtension : MonoBehaviour
{

    public Transform nextRopePosition;
    public Transform groundCheck;
    public LayerMask ground;


    void Start()
    {
        if(!Physics2D.OverlapPoint(groundCheck.position, ground)){
            Invoke("SpawnNextRope",.05f);
        }
    }

    void SpawnNextRope(){
        Instantiate(gameObject, nextRopePosition.position, Quaternion.identity, transform);
    }
}
