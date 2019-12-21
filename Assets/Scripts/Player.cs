﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   Player
/// </summary>
public class Player : SpaceShip
{

#region Fields

    // Player Movement Support
    private Rigidbody rb;

    [SerializeField]
    private float speed = 20;

    // Support shoting bolts
    private GameObject boltPrefab;
    private Timer _cooldownTimer;

    public override Timer CooldownTimer => _cooldownTimer;

#endregion

#region Methods

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _cooldownTimer = gameObject.AddComponent<Timer>();
        _cooldownTimer.Duration = ConfigurationUtils.PlayerShipConfig.cooldown;
        _cooldownTimer.Run();
        boltPrefab = Resources.Load<GameObject>("Prefabs/PlayerBolt");
        speed = ConfigurationUtils.PlayerShipConfig.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Space pressed");
            FireBolt(boltPrefab);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    // Move Player
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        rb.AddForce(speed * direction * rb.mass, ForceMode.Force);

    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }


#endregion
}
