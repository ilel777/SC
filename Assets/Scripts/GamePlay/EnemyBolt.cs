using System.Collections;
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


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy")) return;
        Destroy(gameObject);
    }
}
