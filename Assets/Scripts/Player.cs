using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   Player
/// </summary>
public class Player : MonoBehaviour
{
    #region Fields

    // Player Movement Support
    private Rigidbody rb;
    [SerializeField]
    private float speed = 20;
    [SerializeField]
    private Vector3 movementDirection;

    // Dashing support
    [SerializeField]
    private float dashPower = 3;
    [SerializeField]
    private bool dashing = false;
    [SerializeField]
    private float dashDuration = 0.1f;

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

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
        float movementAngle = Vector3.SignedAngle(direction, Vector3.right, Vector3.up) * Mathf.Deg2Rad;
        Debug.Log("direction angle: " + movementAngle);

        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("dash fired");
            dashing = true;
        }



        if (direction != Vector3.zero)
        {
            // TODO: add a smoothing modifier
            Vector3 movement = (new Vector3(Mathf.Cos(movementAngle), 0, Mathf.Sin(movementAngle)) * speed);

            if (dashing)
            {
                movement *= dashPower;
                StartCoroutine(Dashing());
            }
            // TODO: use normalized direction
            rb.transform.Translate(direction.magnitude * movement * Time.deltaTime);
        }
    }

    private IEnumerator Dashing()
    {
        yield return new WaitForSeconds(dashDuration);
        dashing = false;
    }


    #endregion
}
