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

#endregion

#region Methods

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = ConfigurationUtils.PlayerShipConfig.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Space pressed");
            FireBolt(Resources.Load<GameObject>("Prefabs/PlayerBolt"));
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


#endregion
}
