using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpaceShip
{
    // Support Enemy Dodge
    float _thrustForce, _minDodgeForce, _width;
    Timer _dodgeCooldown;

    #region Properties

    public override Pool<GameObject> Bolts => PoolsContainer.EnemyBolts;

    #endregion

    void Awake()
    {
        base.Awake();
        gameObject.AddComponent<EnemyMovement>();
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        FireRate = 1 / ConfigurationUtils.EnemyShipConfig.cooldown;
        CooldownTimer.Duration = 1 / FireRate;

        BoltThrustForce = ConfigurationUtils.EnemyBoltConfig.impulseForce;

        transform.Rotate(Vector3.up, Mathf.PI * Mathf.Rad2Deg);

    }

    // Update is called once per frame
    void Update()
    {
        FireBolt();

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
