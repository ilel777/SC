using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBolt : Bolt
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        GetComponent<MeshRenderer>().material = new Material(Resources.Load<Material>("Materials/PlayerBolt"));
        tag = "Player Bolt";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Powerup")) return;
        Destroy(gameObject);
    }
}
