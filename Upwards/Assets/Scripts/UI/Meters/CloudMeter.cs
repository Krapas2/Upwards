using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudMeter : MonoBehaviour
{
    private Image meter;
    private PlayerPowerupCloud cloud;
    void Start()
    {
        meter = GetComponent<Image>();
        cloud = FindObjectOfType<PlayerPowerupCloud>();
    }

    void Update()
    {
        meter.fillAmount = (cloud.glideTime-cloud.glideTimer) / cloud.glideTime;
    }
}
