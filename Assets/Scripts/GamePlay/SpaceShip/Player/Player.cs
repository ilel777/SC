using System;
using System.Collections;
using UnityEngine;

/// <summary>
///   Player
/// </summary>
public class Player : SpaceShip
{
    #region Fields

    private PlayerMovement _movement;
    private PlayerAttack _attack;

    #endregion


    #region Properties
    #endregion


    #region Methods

    new void Awake()
    {
        base.Awake();

        // initialize movement component
        _movement = gameObject.AddComponent<PlayerMovement>();

        // initialize attack component
        _attack = gameObject.AddComponent<PlayerAttack>();

        gameObject.AddComponent<PlayerDefence>();
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        // configure movement component
        _movement.Speed = ConfigurationUtils.PlayerShipConfig.speed;


        // configure attack component
        _attack.Power = ConfigurationUtils.PlayerShipConfig.power;
        _attack.FireRate = 1 / ConfigurationUtils.PlayerShipConfig.cooldown;
        _attack.BoltThrustForce = ConfigurationUtils.PlayerBoltConfig.impulseForce;
        _attack.Bolts = PoolsContainer.PlayerBolts;


        Health.LifePoints = ConfigurationUtils.PlayerShipConfig.health;
    }


    /// <summary>
    ///   Handle Collision with other objects
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        Health.TakeDamage((uint)collision.gameObject.GetComponent<Attack>().Power);
        if (Health.IsDestroyed)
        {
            Destroy(gameObject);
            EventManager.TriggerEvent(EventName.PlayerDestroyed, new EventArgs());
        }
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
