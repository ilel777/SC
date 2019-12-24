using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpaceShip
{

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        FireRate = 1 / ConfigurationUtils.EnemyShipConfig.cooldown;
        CooldownTimer.Duration = 1 / FireRate;
        BoltPrefab = Resources.Load<GameObject>("Prefabs/EnemyBolt");
        Speed = ConfigurationUtils.EnemyShipConfig.speed;
    }

    // Update is called once per frame
    void Update()
    {
        FireBolt();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Boundary") || other.gameObject.CompareTag("Enemy Bolt")) return;
        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.EnemyShipDestroyed, new EnemyShipDestroyedEventArgs(ConfigurationUtils.EnemyShipConfig.scoreValue));
    }

    internal override void KeepInsideScreen()
    {
    }

    internal override void Move()
    {
        Rb.AddForce(-Speed * Rb.mass * Vector3.forward);
    }
}
