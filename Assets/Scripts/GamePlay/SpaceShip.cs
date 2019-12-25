using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip : MonoBehaviour
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
    private GameObject _boltPrefab;
    private BoltLauncher _boltLauncher;


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
    public bool ReadyToFire { get => CooldownTimer.Finished || !CooldownTimer.Running; }
    public Timer CooldownTimer { get => _cooldownTimer; }

    // give access to ship stats
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public int Health { get => _health; set => _health = value; }
    public int Power { get => _power; set => _power = value; }

    // Bolt shooting Support
    public GameObject BoltPrefab { get => _boltPrefab; set => _boltPrefab = value; }
    public BoltLauncher BoltLauncher { get => _boltLauncher; set => _boltLauncher = value; }

    #endregion


    // Start is called before the first frame update
    protected void Start()
    {
        //get Rigidbody
        _rb = GetComponent<Rigidbody>();

        // add cooldown timer
        _cooldownTimer = gameObject.AddComponent<Timer>();

        _boltLauncher = GetComponentInChildren<BoltLauncher>();


        // set ship width and height using collider
        SphereCollider collider = GetComponent<SphereCollider>();
        _shipWidth = collider.radius * 2;
        _shipHeight = collider.radius * 2;
    }

    // Update is called once per frame
    void Update()
    {
        FireBolt();
    }

    void FixedUpdate()
    {
        Move();
        KeepInsideScreen();
    }

    internal abstract void KeepInsideScreen();
    internal abstract void Move();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary")) return;
        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.ShipDestroyed, new EventArgs());
    }

    public virtual void FireBolt()
    {
        if (BoltLauncher && ReadyToFire)
        {
            BoltLauncher.LaunchBolt(BoltPrefab);
            CooldownTimer.Duration = 1 / FireRate;
            CooldownTimer.Run();
        }
    }
}
