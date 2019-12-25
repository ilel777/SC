using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpaceShip
{
    // Support Enemy Dodge
    float _thrustForce, _minDodgeForce, _width;
    Timer _dodgeCooldown;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        FireRate = 1 / ConfigurationUtils.EnemyShipConfig.cooldown;
        CooldownTimer.Duration = 1 / FireRate;
        BoltPrefab = Resources.Load<GameObject>("Prefabs/EnemyBolt");
        Speed = ConfigurationUtils.EnemyShipConfig.speed;

        _dodgeCooldown = gameObject.AddComponent<Timer>();
        _dodgeCooldown.Duration = 1.0f;
        _minDodgeForce = 1;
        _width = GetComponent<SphereCollider>().radius * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        FireBolt();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary")
            || other.gameObject.CompareTag("Enemy Bolt")
            || other.gameObject.CompareTag("Powerup")) return;

        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.EnemyShipDestroyed, new EnemyShipDestroyedEventArgs(ConfigurationUtils.EnemyShipConfig.scoreValue));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            EventManager.TriggerEvent(EventName.EnemyShipDestroyed, new EnemyShipDestroyedEventArgs(ConfigurationUtils.EnemyShipConfig.scoreValue));
        }
    }

    internal override void KeepInsideScreen()
    {
    }

    internal override void Move()
    {
        Rb.AddForce(-Speed * Rb.mass * Vector3.forward * Rb.drag);
        if (_dodgeCooldown.Finished || !_dodgeCooldown.Running)
        {
            Dodge();
            _dodgeCooldown.Run();
        }
    }

    void Dodge()
    {
        float rightOrLeft = Random.Range(0, 2);
        Vector3 movementDirection = rightOrLeft == 1 ? Vector3.left : Vector3.right;
        float xDistance = 0;
        if (rightOrLeft == 1)
        {
            xDistance = Mathf.Abs(ScreenUtils.ScreenLeft - (Rb.position.x - _width));
        }
        else
        {
            xDistance = Mathf.Abs(ScreenUtils.ScreenRight - (Rb.position.x + _width));
        }

        _thrustForce = _minDodgeForce < xDistance ? Random.Range(_minDodgeForce, xDistance) : 0;
        Rb.AddRelativeForce(movementDirection * _thrustForce * Rb.mass * Rb.drag, ForceMode.Impulse);
    }

}
