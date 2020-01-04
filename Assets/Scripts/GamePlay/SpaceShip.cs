using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip : MonoBehaviour, ISize
{
    // Support movement
    private Rigidbody _rb;

    // Ship Stats
    float _speed, _fireRate;
    int _health, _power;

    // Support storing ship's with and height
    float _shipWidth;
    float _shipHeight;

    // Support limiting fire rate
    private Timer _cooldownTimer;

    // Support shooting bolts
    private List<BoltLauncher> _boltLaunchers;
    private float _boltThrustForce;
    private int _currentReady;

    #region Properties

    /// <summary>
    ///   Get Rigidbody reference
    /// </summary>
    public Rigidbody Rb { get => _rb; }

    // Support limiting fire rate
    public bool ReadyToFire { get => CooldownTimer.Finished || !CooldownTimer.Running; }
    public Timer CooldownTimer { get => _cooldownTimer; }

    // give access to ship stats
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public int Health { get => _health; set => _health = value; }
    public int Power { get => _power; set => _power = value; }

    // Bolt shooting Support
    public List<BoltLauncher> BoltLaunchers { get => _boltLaunchers; set => _boltLaunchers = value; }
    public float BoltThrustForce { get => _boltThrustForce; set => _boltThrustForce = value; }
    public abstract Pool<GameObject> Bolts { get; }

    #endregion


    // Start is called before the first frame update
    protected void Start()
    {
        //get Rigidbody
        _rb = GetComponent<Rigidbody>();

        // add cooldown timer
        _cooldownTimer = gameObject.AddComponent<Timer>();

        _boltLaunchers = new List<BoltLauncher>();
        _boltLaunchers.AddRange(GetComponentsInChildren<BoltLauncher>()); ;

        StoreShipDimensions();
    }

    // Update is called once per frame
    void Update()
    {
        FireBolt();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary")) return;
        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.ShipDestroyed, new EventArgs());
    }

    public virtual void FireBolt()
    {
        // foreach (BoltLauncher boltLauncher in BoltLaunchers)
        // {
        //     if (ReadyToFire && boltLauncher.ReadyToFire)
        //     {
        //         boltLauncher.LaunchBolt();
        //         CooldownTimer.Duration = 1 / FireRate;
        //         CooldownTimer.Run();
        //         break;
        //     }
        // }

        if (ReadyToFire && BoltLaunchers[_currentReady].ReadyToFire)
        {
            BoltLaunchers[_currentReady].LaunchBolt();
            CooldownTimer.Duration = 1 / FireRate;
            CooldownTimer.Run();
            if (++_currentReady >= BoltLaunchers.Count) _currentReady = 0;
        }
    }

    public float GetWidth()
    {
        if (_shipHeight > 0) return _shipHeight;
        else
        {
            StoreShipDimensions();
        }

        return _shipWidth;
    }

    public float GetHeight()
    {
        if (_shipWidth > 0) return _shipWidth;
        else
        {
            StoreShipDimensions();
        }

        return _shipHeight;
    }

    protected virtual void StoreShipDimensions()
    {
        // set ship width and height using collider
        SphereCollider collider = GetComponent<SphereCollider>();
        _shipWidth = collider.radius * 2 * transform.localScale.x;
        _shipHeight = collider.radius * 2 * transform.localScale.z;
    }

    /// <summary>
    ///   Prepare a new Bolt for the launcher
    /// </summary>
    public virtual GameObject PrepareNewBolt()
    {
        return Bolts.Get();
    }
}
