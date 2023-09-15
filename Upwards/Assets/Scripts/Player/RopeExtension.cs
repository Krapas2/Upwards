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
        if(Physics2D.Linecast(transform.position, groundCheck.position, ground)){
            Destroy(gameObject);
        }else{
            Invoke("SpawnNextRope",.025f);
        }
    }

    void SpawnNextRope(){
        Instantiate(gameObject, nextRopePosition.position, Quaternion.identity, transform);
    }
}
