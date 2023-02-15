using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{


    private PlayerInventory playerInventory;
    private Text text;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = playerInventory.money.ToString();
    }
}
