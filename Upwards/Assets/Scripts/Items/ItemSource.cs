using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSource : MonoBehaviour
{
    public string itemName;
    public ItemDrop itemDrop;
    public int amountDropped;
    public Transform dropPosition;
    private AudioManager _audManager;

    [HideInInspector]
    public Vector3 startPos;

    void Start(){
        startPos = transform.position;
        _audManager = FindObjectOfType<AudioManager>();
    }

    public void Drop(){

        for(int i = 0; i < amountDropped; i++){
            ItemDrop item = Instantiate(itemDrop, dropPosition.position + (new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0f)), Quaternion.identity, transform.parent);
            item.AssignItem(itemName);
        }
        if(_audManager) _audManager.Play("CloudPop");
        Destroy(gameObject);
    }

    public void BreakAnimation(){
        transform.position = startPos + new Vector3(Random.Range(-.125f,.125f),Random.Range(-.125f,.125f),0);
    }
}
