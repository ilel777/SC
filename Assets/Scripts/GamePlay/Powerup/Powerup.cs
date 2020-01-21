using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour, ISize, IConfig
{
    // Support Powerup Effect Duration
    Timer _powerupTimer;

    // store powerup dimensions
    float _width, _height;

    // Support config
    CollectibleConfig _defaultConfig;

    public float EffectDuration { get => 3; }
    public Timer PowerupTimer { get => _powerupTimer; set => _powerupTimer = value; }
    public GameObjectConfig DefaultConfig { get => _defaultConfig; set => _defaultConfig = value as CollectibleConfig; }

    // support movement
    Rigidbody _rb;

    void Awake()
    {
        gameObject.AddComponent<PowerupMovement>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StorePowerupDimensions();
    }

    // Update is called once per frame
    protected void Update()
    {

        // make sure the powerup is out of screen
        if (transform.position.magnitude > (new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenTop)).magnitude * 2)
        {
            Debug.Log(name);
            PoolsContainer.PowerupPools[name].Return(gameObject);
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.TriggerEvent(EventName.PowerupCollected, new PowerupCollectedEventArgs(this));
        }
    }

    public abstract void ApplyEffect(Player player);

    public abstract void DisableEffect(Player player);

    public float GetWidth()
    {
        if (!(_width > 0))
            StorePowerupDimensions();

        return _width;
    }

    public float GetHeight()
    {
        if (!(_height > 0))
            StorePowerupDimensions();

        return _height;
    }

    void StorePowerupDimensions()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        _width = collider.radius * transform.localScale.x;
        _height = collider.height * transform.localScale.z;
    }
}
