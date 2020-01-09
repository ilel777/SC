using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpaceShip
{
    #region Fields

    // Support Enemy Dodge
    float _thrustForce, _minDodgeForce, _width;
    Timer _dodgeCooldown;
    private EnemyMovement _movement;
    private EnemyAttack _attack;

    #endregion


    #region Properties
    #endregion

    new void Awake()
    {
        base.Awake();

        // initialize movement component
        _movement = gameObject.AddComponent<EnemyMovement>();

        // initialize attack component
        _attack = gameObject.AddComponent<EnemyAttack>();
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        // configure movement component
        _movement.Speed = ConfigurationUtils.EnemyShipConfig.speed;

        // configure attack component
        _attack.FireRate = 1 / ConfigurationUtils.EnemyShipConfig.cooldown;
        _attack.BoltThrustForce = ConfigurationUtils.EnemyBoltConfig.impulseForce;
        _attack.Bolts = PoolsContainer.EnemyBolts;

        transform.Rotate(Vector3.up, Mathf.PI * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        // make sure the bolt is out of screen
        if (transform.position.magnitude > (new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenTop)).magnitude * 2)
        {
            PoolsContainer.Enemies.Return(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary")
            || other.gameObject.CompareTag("Enemy Bolt")
            || other.gameObject.CompareTag("Powerup")) return;

        gameObject.SetActive(false);
        PoolsContainer.Enemies.Return(gameObject);
        EventManager.TriggerEvent(EventName.EnemyShipDestroyed, new EnemyShipDestroyedEventArgs(ConfigurationUtils.EnemyShipConfig.scoreValue));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PoolsContainer.Enemies.Return(gameObject);
            EventManager.TriggerEvent(EventName.EnemyShipDestroyed, new EnemyShipDestroyedEventArgs(ConfigurationUtils.EnemyShipConfig.scoreValue));
        }
    }
}
