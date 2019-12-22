using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip : MonoBehaviour
{
    // Support limiting fire rate
    public bool ReadyToFire { get => CooldownTimer.Finished; }
    public abstract Timer CooldownTimer { get; }

    // Support storing ship's with and height
    float _shipWidth;
    float _shipHeight;


    #region Properties

    public float ShipWidth { get => _shipWidth; }
    public float ShipHeight { get => _shipHeight; }

    #endregion


    // Start is called before the first frame update
    protected void Start()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        _shipWidth = collider.radius * 2;
        _shipHeight = collider.radius * 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Boundary")) return;
        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.ShipDestroyed, new EventArgs());
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
