using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBreakSprite : MonoBehaviour
{
    public float breakTime;

    [HideInInspector]
    public bool collectMode = true;

    private PlayerInventory playerInventory;
    private Tilemap tilemap;
    private Camera cam;
    private AudioManager _audManager;
    private bool audBool;

    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        cam = FindObjectOfType<Camera>();
        _audManager = FindObjectOfType<AudioManager>();
        audBool = true;
    }
    void Update()
    {
        ItemSource source = GetSource();

        if(collectMode){
            if(Input.GetButton("Fire1") && source){
                source.BreakAnimation();
                if (audBool) { if(_audManager) _audManager.Play("Break"); 
                    audBool = false; }
                if(!source.IsInvoking("Drop"))
                    source.Invoke("Drop",breakTime);
            }
        }

        if(!source || Input.GetButtonUp("Fire1") || !collectMode){
            foreach(ItemSource itemSource in FindObjectsOfType<ItemSource>()){
                itemSource.transform.position = itemSource.startPos;
                itemSource.CancelInvoke();

                if (_audManager) _audManager.Stop("Break");
                audBool = true;
            }
        }
    }

    private ItemSource GetSource(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        
        if(hit){
            ItemSource source = hit.collider.GetComponent<ItemSource>();
            if (source) {
                return source;
            } else {
                return null;
            }
        } else {
            return null;
        }
    }

    void Drop(ItemSource source){
        source.Drop();
    }

}