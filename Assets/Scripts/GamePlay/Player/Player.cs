using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   Player
/// </summary>
public class Player : SpaceShip
{

    #region Fields
    #endregion


    #region Properties

    public override Pool<GameObject> Bolts => PoolsContainer.PlayerBolts;

    #endregion


    #region Methods

    new void Awake()
    {
        base.Awake();
        gameObject.AddComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        FireRate = 1 / ConfigurationUtils.PlayerShipConfig.cooldown;
        CooldownTimer.Duration = 1 / FireRate;

        BoltThrustForce = ConfigurationUtils.PlayerBoltConfig.impulseForce;

        // Speed = ConfigurationUtils.PlayerShipConfig.speed;
    }

    public override void FireBolt()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Space pressed");
            base.FireBolt();
        }
    }

    /// <summary>
    ///   Handle Collision with other objects
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.PlayerDestroyed, new EventArgs());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bolt")
            || other.gameObject.CompareTag("Powerup")) return;

        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.PlayerDestroyed, new EventArgs());
    }

    #endregion
}
