using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip : MonoBehaviour
{
    // Support limiting fire rate
    public bool ReadyToFire { get => CooldownTimer.Finished; }
    public abstract Timer CooldownTimer { get; }

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
        if (other.gameObject.CompareTag("Boundary")) return;
        Destroy(gameObject);
        EventManager.Invoke(EventName.ShipDestroyed);
    }

    public void FireBolt(GameObject boltPrefab)
    {
        BoltLauncher boltLauncher = GetComponentInChildren<BoltLauncher>();
        if (boltLauncher && ReadyToFire)
        {
            boltLauncher.LaunchBolt(boltPrefab);
            CooldownTimer.Run();
        }
    }
}
