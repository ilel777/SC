using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerup : Powerup
{

    public override void ApplyEffect(Player player)
    {
        Debug.Log("Applaying Effects");
        player.GetComponent<Attack>().FireRate *= 5;
    }

    public override void DisableEffect(Player player)
    {
        Debug.Log("Removing Effects");
        player.GetComponent<Attack>().FireRate /= 5;
    }
}
