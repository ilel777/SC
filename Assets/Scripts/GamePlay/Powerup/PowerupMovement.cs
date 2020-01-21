using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMovement : Movement
{
    #region Fields

    private Rigidbody _rb;
    private CollectibleConfig _config;

    #endregion

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _rb = GetComponent<Rigidbody>();
        _config = GetComponent<IConfig>().DefaultConfig as CollectibleConfig;
        yield return new WaitUntil(() => _config != null);
        ConfigureMovement();
    }

    protected override void Move()
    {
        _rb.AddForce(-Speed * _rb.mass * Vector3.forward);
    }

    protected override void ConfigureMovement()
    {
        if (_config != null)
            Speed = _config.movement.speed;
    }
}
