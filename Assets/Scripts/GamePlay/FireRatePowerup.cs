using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerup : Powerup
{

    // Start is called before the first frame update
    new void Start()
    {
    }

    // Update is called once per frame
    new void Update()
    {
    }

    public override void ApplyEffect(Player player)
    {
        Debug.Log("Applaying Effects");
        player.FireRate *= 5;
    }

    public override void DisableEffect(Player player)
    {
        Debug.Log("Removing Effects");
        player.FireRate /= 5;
    }
}
