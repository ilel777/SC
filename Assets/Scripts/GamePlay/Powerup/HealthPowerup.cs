using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : Powerup
{
    public override void ApplyEffect(Player player)
    {
        player.GetComponent<Health>().Recover(50);
    }

    public override void DisableEffect(Player player)
    {
    }
}
