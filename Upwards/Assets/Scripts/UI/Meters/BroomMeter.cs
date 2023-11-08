using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomMeter : PowerupMeter
{
    private PlayerPowerupBroom broom;
    void Start()
    {
        Setup();
        broom = FindObjectOfType<PlayerPowerupBroom>();
    }

    public override float FillAmount()
    {
        return (broom.flightTime - broom.flightTimer) / broom.flightTime;
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
