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


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        FireRate = 1 / ConfigurationUtils.EnemyShipConfig.cooldown;
        CooldownTimer.Duration = 1 / FireRate;

        BoltThrustForce = ConfigurationUtils.EnemyBoltConfig.impulseForce;

        Speed = ConfigurationUtils.EnemyShipConfig.speed;

        _dodgeCooldown = gameObject.AddComponent<Timer>();
        _dodgeCooldown.Duration = 1.0f;
        _minDodgeForce = 1;

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

    internal override void KeepInsideScreen()
    {
        Debug.Log(GetWidth());
        Rb.position = new Vector3(
            Mathf.Clamp(Rb.position.x, ScreenUtils.ScreenLeft + (GetWidth() / 2), ScreenUtils.ScreenRight - (GetWidth() / 2)),
            Rb.position.y,
            Rb.position.z
            );
    }

    internal override void Move()
    {
        Rb.AddRelativeForce(Speed * Rb.mass * Vector3.forward * Rb.drag);
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
            xDistance = Mathf.Abs(ScreenUtils.ScreenLeft - (Rb.position.x - GetWidth()));
        }
        else
        {
            xDistance = Mathf.Abs(ScreenUtils.ScreenRight - (Rb.position.x + GetHeight()));
        }

        _thrustForce = _minDodgeForce < xDistance ? Random.Range(_minDodgeForce, xDistance) : 0;
        Rb.AddRelativeForce(movementDirection * _thrustForce * Rb.mass * Rb.drag, ForceMode.Impulse);
    }
}
