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
    [SerializeField]
    private float speed = 20;

    // Support shoting bolts
    private GameObject boltPrefab;
    private Timer _cooldownTimer;



    #endregion


    #region Properties

    public override Timer CooldownTimer => _cooldownTimer;

    #endregion


    #region Methods

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
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

    /// <summary>
    ///   Handle Collision with other objects
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Boundary") || other.gameObject.CompareTag("Player Bolt")) return;
        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.PlayerDestroyed, new EventArgs());
    }


    /// <summary>
    ///   Keep Player Ship inside screen Boundaries
    /// </summary>
    void KeepInsideScreen()
    {
        Rb.position = new Vector3(
                                  Mathf.Clamp(Rb.position.x, ScreenUtils.ScreenLeft + (ShipWidth / 2), ScreenUtils.ScreenRight - (ShipWidth / 2)),
                                  Rb.position.y,
                                  Mathf.Clamp(Rb.position.z, ScreenUtils.ScreenBottom + (ShipHeight / 2), ScreenUtils.ScreenTop - (ShipHeight / 2))
                                  );
    }

    // Move Player
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        Rb.AddForce(speed * direction * Rb.mass, ForceMode.Force);

    }

    #endregion
}
