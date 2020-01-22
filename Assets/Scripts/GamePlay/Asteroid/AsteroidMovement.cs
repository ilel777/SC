using System.Collections;
using UnityEngine;

public class AsteroidMovement : Movement
{
    #region Fields

    private Rigidbody _rb;
    private Vector3 _torqueVector;
    private float _rotationSpeed;
    private ObstacleConfig _config;

    #endregion
    void Awake()
    {
    }

    IEnumerator Start()
    {
        _rb = GetComponent<Rigidbody>();
        _config = GetComponent<Asteroid>().DefaultConfig as ObstacleConfig;
        yield return new WaitUntil(() => _config != null);
        ConfigureMovement();
    }

    protected override void ConfigureMovement()
    {
        _torqueVector = UnityEngine.Random.insideUnitSphere.normalized;
        if (_config != null)
        {
            _rotationSpeed = _config.movement.rotationSpeed;
            Speed = _config.movement.speed;
        }
    }

    protected override void Move()
    {
        _rb.AddTorque(_torqueVector * _rotationSpeed * _rb.mass);
        _rb.AddForce(-Speed * _rb.mass * Vector3.forward);
    }
}
