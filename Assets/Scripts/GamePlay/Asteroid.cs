using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float rotationSpeed;
    private Vector3 torqueVector;
    private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        torqueVector = Random.insideUnitSphere.normalized;
        rotationSpeed = ConfigurationUtils.AsteroidConfig.rotationSpeed;
        _speed = ConfigurationUtils.AsteroidConfig.speed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        rb.AddTorque(torqueVector * rotationSpeed * rb.mass);
        rb.AddForce(-_speed * rb.mass * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player Bolt")
            || other.gameObject.CompareTag("Boundary"))
        {
            Destroy(gameObject);
            EventManager.TriggerEvent(EventName.AsteroidDestroyed, new AsteroidDestroyedEventArgs(ConfigurationUtils.AsteroidConfig.scoreValue));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            EventManager.TriggerEvent(EventName.AsteroidDestroyed, new AsteroidDestroyedEventArgs(ConfigurationUtils.AsteroidConfig.scoreValue));
        }
    }

}
