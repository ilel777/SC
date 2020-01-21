using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : Movement
{
    #region Fields

    private Rigidbody _rb;
    private Vector3 _torqueVector;
    private float _rotationSpeed;
    private AsteroidConfig _config;

    #endregion
    void Awake()
    {
    }

    IEnumerator Start()
    {
        _rb = GetComponent<Rigidbody>();
        yield return new WaitUntil(() => GetComponent<Asteroid>().DefaultConfig != null);
        _config = GetComponent<Asteroid>().DefaultConfig as AsteroidConfig;
        ConfigureMovement();
    }

    protected override void ConfigureMovement()
    {
        _torqueVector = UnityEngine.Random.insideUnitSphere.normalized;
        if (_config != null)
        {
            _rotationSpeed = _config.movement.rotationSpeed;
            Speed = _config.movement.speed;
            Debug.Log(Speed);
        }
    }

    protected override void Move()
    {
        _rb.AddTorque(_torqueVector * _rotationSpeed * _rb.mass);
        _rb.AddForce(-Speed * _rb.mass * Vector3.forward);
    }
}
