using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    #region Fields

    private Rigidbody _rb;
    private ISize _size;
    private EnemyShipConfig _config;
    private Timer _dodgeCooldown;
    private float _thrustForce;
    private float _minDodgeForce;
    private float _maxDodgeForce;

    #endregion

    IEnumerator Start()
    {
        _rb = GetComponent<Rigidbody>();
        _size = GetComponent<ISize>();
        _config = GetComponent<IConfig>().DefaultConfig as EnemyShipConfig;

        _dodgeCooldown = gameObject.AddComponent<Timer>();
        // _dodgeCooldown.Duration = 1.0f;
        // _minDodgeForce = 10;
        // _maxDodgeForce = 30;

        yield return new WaitUntil(() => _config != null);

        ConfigureMovement();
    }

    protected override void ConfigureMovement()
    {
        // configure movement component
        if (_config != null)
        {
            Speed = _config.movement.speed;
            _dodgeCooldown.Duration = _config.movement.dodgeCooldown;
            _minDodgeForce = _config.movement.minDodgeForce;
            _maxDodgeForce = _config.movement.maxDodgeForce;

        }
    }

    protected override void Move()
    {
        _rb.AddRelativeForce(Speed * _rb.mass * Vector3.forward * _rb.drag);
        if (_dodgeCooldown.Finished || !_dodgeCooldown.Running)
        {
            Dodge();
            _dodgeCooldown.Run();
        }
    }

    void KeepInsideScreen()
    {
        _rb.position = new Vector3(
            Mathf.Clamp(_rb.position.x, ScreenUtils.ScreenLeft + (_size.GetWidth() / 2), ScreenUtils.ScreenRight - (_size.GetWidth() / 2)),
            _rb.position.y,
            _rb.position.z
            );
    }

    void Dodge()
    {
        float rndLocation = UnityEngine.Random.Range(ScreenUtils.ScreenLeft + _size.GetWidth(), ScreenUtils.ScreenRight - _size.GetWidth());
        Vector3 movementDirection = rndLocation < _rb.position.x ? Vector3.right : Vector3.left;
        float xDistance = Mathf.Abs(rndLocation - _rb.position.x);

        float thrustForce, minThrustForce, maxThrustForce;
        minThrustForce = _minDodgeForce < xDistance ? _minDodgeForce : 0;
        maxThrustForce = _maxDodgeForce > xDistance ? xDistance : _maxDodgeForce;

        thrustForce = UnityEngine.Random.Range(minThrustForce, maxThrustForce);

        _rb.AddRelativeForce(movementDirection * thrustForce * _rb.mass * _rb.drag, ForceMode.Impulse);
    }

}
