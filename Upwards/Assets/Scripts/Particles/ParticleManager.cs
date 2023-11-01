using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(DestroyParticles());
    }
}
