using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    
    public float sellRadius = 2;
    public LayerMask player;

    public SpriteRenderer prompt;

    private PlayerInventory inventory;
    private AudioManager audioManager;

    void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        bool isClose = Physics2D.OverlapCircle(transform.position, sellRadius, player);

        prompt.enabled = isClose;
        
        if (isClose)
        {
            if (Input.GetButtonDown("Interact"))
            {
                int moneyAdded = 0;
                for(int i = 0; i < inventory.items.Length; i++){
                    moneyAdded += inventory.items[i].amount * inventory.items[i].unitValue;
                    inventory.items[i].amount = 0;
                }
                if(moneyAdded > 0){
                    inventory.money += moneyAdded;
                    audioManager.Play("arrSell");
                } else
                    audioManager.Play("Arr");
            }
        }
    }
}
