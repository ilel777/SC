using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        EventManager.Invoke(EventName.ShipDestroyed);
    }

    public void FireBolt(GameObject boltPrefab)
    {
        BoltLauncher boltLauncher = GetComponentInChildren<BoltLauncher>();
        if (boltLauncher)
        {
            boltLauncher.LaunchBolt(boltPrefab);
        }
    }
}
