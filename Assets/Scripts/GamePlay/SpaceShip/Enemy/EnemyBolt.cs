using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : Bolt
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        GetComponent<MeshRenderer>().material = new Material(Resources.Load<Material>("Materials/EnemyBolt"));
        tag = "Enemy Bolt";
        gameObject.layer = 10;

        Attack.Power = ConfigurationUtils.EnemyBoltConfig.power;
    }


    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player") ||
    //         other.gameObject.CompareTag("Player Bolt") ||
    //         other.gameObject.CompareTag("Boundary"))
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    void OnCollisionEnter(Collision collision)
    {
        PoolsContainer.EnemyBolts.Return(gameObject);
        Destroy(Instantiate(ExplosionPrefab, transform.position, Quaternion.identity), 3.0f);
    }
}
