using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTracker : MonoBehaviour
{

    [System.Serializable]
    public struct Tracker
    {
        public string name;
        public GameObject tracker;
        [HideInInspector]
        public Text[] texts;
    }

    public Tracker[] trackers;

    private PlayerInventory inventory;

    void Start()
    {
        for(int i = 0; i < trackers.Length; i++){
            trackers[i].texts = trackers[i].tracker.GetComponentsInChildren<Text>();
        }

        inventory = FindObjectOfType<PlayerInventory>();
    }

    void Update()
    {
        foreach(Tracker tracker in trackers){
            int itemIndex = inventory.ItemIndexFromName(tracker.name);
            
            foreach(Text text in tracker.texts){
                text.text = inventory.items[itemIndex].amount.ToString();
            }
            tracker.tracker.SetActive(inventory.lastAddedIndex == itemIndex);
        }
    }
}
