using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    // Support Powerup Effect Duration
    Timer _powerupTimer;

    public float EffectDuration { get => 3; }
    public Timer PowerupTimer { get => _powerupTimer; set => _powerupTimer = value; }


    // Start is called before the first frame update
    protected virtual void Start()
    {
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

}
