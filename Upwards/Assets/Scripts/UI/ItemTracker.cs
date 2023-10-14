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

    public float showTime;

    private PlayerInventory inventory;
    private PlayerPowerupManager powerupManager;

    void Start()
    {
        for (int i = 0; i < trackers.Length; i++)
        {
            trackers[i].texts = trackers[i].tracker.GetComponentsInChildren<Text>();
        }
        inventory = FindObjectOfType<PlayerInventory>();
        powerupManager = FindObjectOfType<PlayerPowerupManager>();

        ShowTrackers();
    }

    void Update()
    {
        UpdateTrackers();
        if (Input.GetButton("ShowUI"))
        {
            ShowTrackers();
        }
    }

    void UpdateTrackers(){
        foreach(Tracker tracker in trackers){
            UpdateTracker(tracker);
        }
    }

    void UpdateTracker(Tracker tracker){
        int itemIndex = inventory.ItemIndexFromName(tracker.name);
        int powerupIndex = powerupManager.PowerupIndexFromItemName(tracker.name);
        foreach(Text text in tracker.texts){
            string itemAmount = inventory.items[itemIndex].amount.ToString();
            string itemMax = powerupManager.powerups[powerupIndex].ItemRequiredAmount.ToString();
            text.text = string.Concat(itemAmount, " / ", itemMax);
        }
    }

    void ShowTrackers()
    {
        CancelInvoke();
        SetTrackersEnabled(true);
        Invoke("HideTrackers", showTime);
    }

    void HideTrackers()
    {
        SetTrackersEnabled(false);
    }

    void SetTrackersEnabled(bool enabled)
    {
        foreach (Tracker tracker in trackers)
        {
            tracker.tracker.SetActive(enabled);
        }
    }
}
