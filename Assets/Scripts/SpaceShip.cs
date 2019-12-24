using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip : MonoBehaviour
{
    // Support movement
    private Rigidbody _rb;


    // Support storing ship's with and height
    float _shipWidth;
    float _shipHeight;

    // Support limiting fire rate
    float _fireRate;

    #region Properties

    /// <summary>
    ///   Get Ship's width and height
    /// </summary>
    public float ShipWidth { get => _shipWidth; }
    public float ShipHeight { get => _shipHeight; }

    /// <summary>
    ///   Get Rigidbody reference
    /// </summary>
    public Rigidbody Rb { get => _rb; }

    // Support limiting fire rate
    public bool ReadyToFire { get => CooldownTimer.Finished; }
    public abstract Timer CooldownTimer { get; }
    public float FireRate { get => _fireRate; set => _fireRate = value; }

    #endregion


    // Start is called before the first frame update
    protected void Start()
    {
        //get Rigidbody
        _rb = GetComponent<Rigidbody>();

        // set ship width and height using collider
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
            CooldownTimer.Duration = 1 / FireRate;
            CooldownTimer.Run();
        }
    }
}
