using System;
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
        base.Start();
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
        KeepInsideScreen();
    }

    void KeepInsideScreen()
    {
        rb.position = new Vector3(
                                  Mathf.Clamp(rb.position.x, ScreenUtils.ScreenLeft + (ShipWidth / 2), ScreenUtils.ScreenRight - (ShipWidth / 2)),
                                  rb.position.y,
                                  Mathf.Clamp(rb.position.z, ScreenUtils.ScreenBottom + (ShipHeight / 2), ScreenUtils.ScreenTop - (ShipHeight / 2))
                                  );
    }

    // Move Player
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        rb.AddForce(speed * direction * rb.mass, ForceMode.Force);

    }

    #endregion
}
