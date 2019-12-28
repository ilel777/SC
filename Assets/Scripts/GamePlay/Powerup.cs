using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour, ISize
{
    // Support Powerup Effect Duration
    Timer _powerupTimer;

    // store powerup dimensions
    float _width, _height;

    public float EffectDuration { get => 3; }
    public Timer PowerupTimer { get => _powerupTimer; set => _powerupTimer = value; }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        StorePowerupDimensions();
    }

    // Update is called once per frame
    protected void Update()
    {
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
