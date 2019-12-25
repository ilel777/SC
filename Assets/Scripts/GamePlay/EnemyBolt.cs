﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : Bolt
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ShotForce = ConfigurationUtils.EnemyBoltConfig.impulseForce;
        Rb.AddForce(-Vector3.forward * Rb.mass * ShotForce, ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") ||
            other.gameObject.CompareTag("Player Bolt") ||
            other.gameObject.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }

}
