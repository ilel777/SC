using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAttack : Attack
{
    #region Fields

    // Support attack Power
    uint _power;

    // Support limiting fire rate
    private Timer _cooldownTimer;
    private float _fireRate;

    // Support shooting bolts
    private List<BoltLauncher> _boltLaunchers;
    private Pool<GameObject> _bolts;
    private float _boltThrustForce;
    private int _currentReady;

    #endregion


    #region Properties

    public uint Power { get => _power; set => _power = value; }

    // Bolt shooting Support
    public List<BoltLauncher> BoltLaunchers { get => _boltLaunchers; set => _boltLaunchers = value; }
    public float BoltThrustForce { get => _boltThrustForce; set => _boltThrustForce = value; }
    public Pool<GameObject> Bolts { get => _bolts; set => this._bolts = value; }

    // Support limiting fire rate
    public bool ReadyToFire { get => CooldownTimer.Finished || !CooldownTimer.Running; }
    public Timer CooldownTimer { get => _cooldownTimer; }

    public float FireRate { get => _fireRate; set => _fireRate = value; }

    #endregion


    #region Methods

    protected virtual void Awake()
    {
        // add cooldown timer
        _cooldownTimer = gameObject.AddComponent<Timer>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _boltLaunchers = new List<BoltLauncher>();
        _boltLaunchers.AddRange(GetComponentsInChildren<BoltLauncher>()); ;

        CooldownTimer.Duration = 1 / FireRate;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FireBolt();
    }


    public virtual void FireBolt()
    {
        if (ReadyToFire && BoltLaunchers[_currentReady].ReadyToFire)
        {
            BoltLaunchers[_currentReady].LaunchBolt();
            CooldownTimer.Duration = 1 / FireRate;
            CooldownTimer.Run();
            if (++_currentReady >= BoltLaunchers.Count) _currentReady = 0;
        }
    }

    /// <summary>
    ///   Prepare a new Bolt for the launcher
    /// </summary>
    public virtual GameObject PrepareNewBolt()
    {
        return Bolts.Get();
    }

    #endregion
}
