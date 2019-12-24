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
    #endregion


    #region Properties
    #endregion


    #region Methods

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        FireRate = 1 / ConfigurationUtils.PlayerShipConfig.cooldown;
        CooldownTimer.Duration = 1 / FireRate;
        BoltPrefab = Resources.Load<GameObject>("Prefabs/PlayerBolt");
        Speed = ConfigurationUtils.PlayerShipConfig.speed;
    }

    public override void FireBolt()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Space pressed");
            base.FireBolt();
        }
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
    internal override void KeepInsideScreen()
    {
        Rb.position = new Vector3(
                                  Mathf.Clamp(Rb.position.x, ScreenUtils.ScreenLeft + (ShipWidth / 2), ScreenUtils.ScreenRight - (ShipWidth / 2)),
                                  Rb.position.y,
                                  Mathf.Clamp(Rb.position.z, ScreenUtils.ScreenBottom + (ShipHeight / 2), ScreenUtils.ScreenTop - (ShipHeight / 2))
                                  );
    }

    // Move Player
    internal override void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // horizontalInput = Mathf.Sign(horizontalInput) * (Mathf.Ceil(Mathf.Abs(horizontalInput)));
        // verticalInput = Mathf.Sign(verticalInput) * (Mathf.Ceil(Mathf.Abs(verticalInput)));

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (direction == Vector3.zero) Rb.drag = 10;
        else Rb.drag = 1;

        Rb.AddForce(Speed * direction * Rb.mass, ForceMode.Force);

    }

    #endregion
}
