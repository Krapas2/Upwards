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

    void Start()
    {
        for (int i = 0; i < trackers.Length; i++)
        {
            trackers[i].texts = trackers[i].tracker.GetComponentsInChildren<Text>();
        }
        inventory = FindObjectOfType<PlayerInventory>();

        ShowTrackers();
    }

    void Update()
    {
        if (Input.GetButton("ShowUI"))
        {
            ShowTrackers();
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
