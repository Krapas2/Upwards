using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMeter : PowerupMeter
{
    private PlayerPowerupCloud cloud;
    void Start()
    {
        Setup();
        cloud = FindObjectOfType<PlayerPowerupCloud>();
    }

    public override float FillAmount()
    {
        return (cloud.glideTime-cloud.glideTimer) / cloud.glideTime;
    }
}
