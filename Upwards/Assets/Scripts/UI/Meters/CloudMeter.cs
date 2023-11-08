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
        return (cloud.glideTime - cloud.glideTimer) / cloud.glideTime;
    }

    public override bool Show()
    {
        return Input.GetButtonDown(powerupButton) && Input.GetButtonDown("Jump");
    }
    public override bool Hide()
    {
        return Input.GetButtonUp(powerupButton) || Input.GetButtonUp("Jump");
    }
}
