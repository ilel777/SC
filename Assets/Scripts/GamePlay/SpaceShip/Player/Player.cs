﻿using System;
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
    public new PlayerShipConfig DefaultConfig { get => base.DefaultConfig as PlayerShipConfig; }
    #endregion


    #region Methods

    new void Awake()
    {
        base.Awake();

        // base.DefaultConfig = ConfigurationUtils.PlayerShipConfig;

        // initialize movement component
        _movement = gameObject.AddComponent<PlayerMovement>();

        // initialize attack component
        _attack = gameObject.AddComponent<PlayerAttack>();

        gameObject.AddComponent<PlayerDefence>();
    }

    // Start is called before the first frame update
    new IEnumerator Start()
    {
        yield return base.Start();

        // configure attack component
        _attack.Power = DefaultConfig.attack.power;
        _attack.FireRate = 1 / DefaultConfig.attack.cooldown;
        _attack.BoltThrustForce = DefaultBoltConfig.movement.speed;
        _attack.Bolts = PoolsContainer.BoltPools[DefaultBoltConfig.name];

        Health.LifePoints = DefaultConfig.health.lifePoints;
    }


    /// <summary>
    ///   Handle Collision with other objects
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        Health.TakeDamage((uint)collision.gameObject.GetComponent<Attack>().Power);
        if (Health.IsDestroyed)
        {
            // Destroy(gameObject);
            PoolsContainer.SpaceShipPools[name].Return(gameObject);
            GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 3.0f);
            EventManager.TriggerEvent(EventName.PlayerDestroyed, new EventArgs());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bolt")
            || other.gameObject.CompareTag("Powerup")) return;

        // Destroy(gameObject);
        PoolsContainer.SpaceShipPools[name].Return(gameObject);
        EventManager.TriggerEvent(EventName.PlayerDestroyed, new EventArgs());
    }

    #endregion
}
