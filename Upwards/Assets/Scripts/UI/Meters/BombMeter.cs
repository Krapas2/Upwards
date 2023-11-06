using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombMeter : MonoBehaviour
{
    private Image meter;
    private PlayerPowerupBomb bomb;
    void Start()
    {
        meter = GetComponent<Image>();
        bomb = FindObjectOfType<PlayerPowerupBomb>();
    }

    void Update()
    {
        meter.fillAmount = (float)(bomb.bombNumber-bomb.counter) / (float)(bomb.bombNumber);
    }
}
