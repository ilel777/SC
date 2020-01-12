using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, ISize
{
    // Movement Support
    private Rigidbody rb;
    [SerializeField]
    private float rotationSpeed;
    private Vector3 torqueVector;
    private float _speed;

    // Support Health Stats
    private Health _health;

    // holds Asteroid width and hight
    float _width, _height;

    // Support damage
    Attack _attack;

    // Support Explosion
    [SerializeField]
    GameObject _explosionPrefab;

    public Health Health { get => _health; }

    void Awake()
    {
        gameObject.AddComponent<AsteroidMovement>();
        _health = gameObject.AddComponent<Health>();
        _attack = gameObject.AddComponent<Attack>();
        _explosionPrefab = Resources.Load<GameObject>("Prefabs/Asteroid Explosion");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // extract the size from the Collider
        StoreAsteroidDimensions();
    }

    void OnEnable()
    {
        _health.LifePoints = ConfigurationUtils.AsteroidConfig.health;

        _attack.Power = ConfigurationUtils.AsteroidConfig.power;
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

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player Bolt")
    //         || other.gameObject.CompareTag("Boundary"))
    //     {
    //         PoolsContainer.Asteroids.Return(gameObject);
    //         EventManager.TriggerEvent(EventName.AsteroidDestroyed, new AsteroidDestroyedEventArgs(ConfigurationUtils.AsteroidConfig.scoreValue));
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        Health.TakeDamage((uint)collision.gameObject.GetComponent<Attack>().Power);
        Debug.Log(Health.LifePoints);
        if (Health.IsDestroyed)
        {
            PoolsContainer.Asteroids.Return(gameObject);
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, _explosionPrefab.transform.rotation);
            explosion.transform.localScale *= 3;
            Destroy(explosion, 3.0f);
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
