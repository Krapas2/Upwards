using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCloudSource : MonoBehaviour
{
    public EndCloudDrop endCloudDrop;
    public float breakTime;
    public float breakShakeStrength;
    public int particleAmount;

    [HideInInspector]
    public Vector3 startPos;

    private bool breaking;
    private Camera cam;

    void Start(){
        cam = Camera.main;

        startPos = transform.position;
        breaking = false;

        breakShakeStrength /= 2;
    }

    void Update(){
        if(Input.GetButtonDown("Fire1") && !breaking){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, (1 << gameObject.layer));
            if(hit){
                if(hit.collider.gameObject == gameObject){
                    breaking = true;
                    StartCoroutine(BreakAnimation());
                }
            }
        } else if(Input.GetButtonUp("Fire1")){
            StopAllCoroutines();
            transform.position = startPos;
            breaking = false;
        }
    }

    public void Drop(){
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Vector3 maxPos = sprite.bounds.max - transform.position;
        Vector3 minPos = sprite.bounds.min - transform.position;
        for(int i = 0; i < particleAmount; i++ ){
            Instantiate(endCloudDrop, transform.position + new Vector3(Random.Range(minPos.x,maxPos.x), Random.Range(minPos.y,maxPos.x)), Quaternion.identity, transform.parent);
        }
        FindObjectOfType<EndCloudManager>().particleAmount = particleAmount;
        Destroy(gameObject);
    }

    IEnumerator BreakAnimation()
    {
        for (float timer = 0f; timer < breakTime; timer += Time.deltaTime)
        {
            float curBreakShakeStrength = breakShakeStrength * Mathf.Pow(timer/breakTime,2);
            transform.position = startPos + new Vector3(Random.Range(-curBreakShakeStrength,curBreakShakeStrength),Random.Range(-curBreakShakeStrength,curBreakShakeStrength),0);
            yield return null;
        }
        Drop();
        breaking = false;
    }
}
