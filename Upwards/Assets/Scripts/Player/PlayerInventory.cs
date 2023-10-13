using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public struct Item
    {
        public string name;
        public int amount;
        public int unitValue;
    }

    public int money;
    public Item[] items;

    [HideInInspector]
    public int lastAddedIndex = -1;


    public int ItemIndexFromName(string checkName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (checkName == items[i].name)
            {
                Debug.Log(items[i].name);
                return i;
            }
        }
        return -1;
    }

    public string ItemNameFromIndex(int index)
    {
        return items[index].name;
    }

    public void AddItem(int index, int amount)
    {
        items[index].amount += amount;
        lastAddedIndex = index;
    }
}

