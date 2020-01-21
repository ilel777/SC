﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMovement : Movement
{
    #region Fields

    private Rigidbody _rb;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        Speed = ConfigurationUtils.CollectiblesConfig[0].speed;
    }

    protected override void Move()
    {
        _rb.AddForce(-Speed * _rb.mass * Vector3.forward);
    }

}
