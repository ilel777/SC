using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : Movement
{
    #region Fields

    private Rigidbody _rb;
    private Vector3 _torqueVector;
    private float _rotationSpeed;

    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        _torqueVector = Random.insideUnitSphere.normalized;
        _rotationSpeed = ConfigurationUtils.AsteroidConfig.rotationSpeed;
        Speed = ConfigurationUtils.AsteroidConfig.speed;
    }

    protected override void Move()
    {
        _rb.AddTorque(_torqueVector * _rotationSpeed * _rb.mass);
        _rb.AddForce(-Speed * _rb.mass * Vector3.forward);
    }
}
