using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMeter : PowerupMeter
{
    private PlayerPowerupBomb bomb;
    void Start()
    {
        Setup();
        bomb = FindObjectOfType<PlayerPowerupBomb>();
    }

    public override float FillAmount()
    {
        return (float)(bomb.bombNumber - bomb.counter) / (float)(bomb.bombNumber);
    }


    public override bool Show()
    {
        return Input.GetButtonDown(powerupButton);
    }
    public override bool Hide()
    {
        return Input.GetButtonUp(powerupButton);
    }
}
