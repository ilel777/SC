using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float rotationSpeed;
    private Vector3 torqueVector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        torqueVector = Random.insideUnitSphere.normalized;
        rotationSpeed = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        rb.AddTorque(torqueVector * rotationSpeed * rb.mass);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player Bolt") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            EventManager.Invoke(EventName.AsteroidDestroyed);
        }
    }

}
