using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, ISize
{
    private Rigidbody rb;
    [SerializeField]
    private float rotationSpeed;
    private Vector3 torqueVector;
    private float _speed;

    // holds Asteroid width and hight
    float _width, _height;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.AddComponent<AsteroidMovement>();

        // extract the size from the Collider
        StoreAsteroidDimensions();
    }

    // Update is called once per frame
    void Update()
    {
        // make sure the asteroid is out of screen
        if (transform.position.magnitude > (new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenTop)).magnitude * 2)
        {
            PoolsContainer.Asteroids.Return(gameObject);
        }
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("Player Bolt")
            || other.gameObject.CompareTag("Boundary"))
        {
            PoolsContainer.Asteroids.Return(gameObject);
            EventManager.TriggerEvent(EventName.AsteroidDestroyed, new AsteroidDestroyedEventArgs(ConfigurationUtils.AsteroidConfig.scoreValue));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PoolsContainer.Asteroids.Return(gameObject);
            EventManager.TriggerEvent(EventName.AsteroidDestroyed, new AsteroidDestroyedEventArgs(ConfigurationUtils.AsteroidConfig.scoreValue));
        }
    }

    public float GetWidth()
    {
        if (!(_width > 0))
            StoreAsteroidDimensions();

        return _width;
    }

    public float GetHeight()
    {
        if (!(_height > 0))
            StoreAsteroidDimensions();

        return _height;
    }

    void StoreAsteroidDimensions()
    {
        // extract the size from the Collider
        BoxCollider collider = GetComponent<BoxCollider>();
        _width = collider.size.x * transform.localScale.x;
        _height = collider.size.z * transform.localScale.z;
    }
}
