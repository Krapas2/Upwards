using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour
{
    //-------------------inventario do player-------------------
    private PlayerInventory inventory;

    //-------------------checkando objeto-------------------
    public Transform touchCheck;
    public LayerMask obj;
    public int ItemIndex;

    void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
    }

    void Update()
    {
        bool isOnResource = Physics2D.OverlapCircle(touchCheck.position, 5f, obj);

        if (isOnResource)
        {
            if (Input.GetKey(KeyCode.K))
            {
                if (inventory.items[ItemIndex].amount > 0)
                {
                    inventory.money += (inventory.items[ItemIndex].unitValue * inventory.items[ItemIndex].amount);
                    inventory.items[ItemIndex].amount = 0;
                }
            }
        }
    }
}
