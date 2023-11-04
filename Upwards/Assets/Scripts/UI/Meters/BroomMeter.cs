using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BroomMeter : MonoBehaviour
{
    private Image meter;
    private PlayerPowerupBroom broom;
    void Start()
    {
        meter = GetComponent<Image>();
        broom = FindObjectOfType<PlayerPowerupBroom>();
    }

    void Update()
    {
        meter.fillAmount = (broom.flightTime-broom.flightTimer) / broom.flightTime;
    }
}
