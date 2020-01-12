using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip : MonoBehaviour, ISize
{
    // Support movement
    private Rigidbody _rb;

    // Ship Stats
    float _speed;
    int _power;
    private Health _health;

    // Support storing ship's with and height
    float _shipWidth;
    float _shipHeight;

    // Support Explosion
    GameObject _explosionPrefab;

    #region Properties

    /// <summary>
    ///   Get Rigidbody reference
    /// </summary>
    public Rigidbody Rb { get => _rb; }


    // give access to ship stats

    public float Speed { get => _speed; set => _speed = value; }
    public int Power { get => _power; set => _power = value; }
    public Health Health { get => _health; }
    public GameObject ExplosionPrefab { get => _explosionPrefab; set => _explosionPrefab = value; }


    #endregion

    protected void Awake()
    {
        _health = gameObject.AddComponent<Health>();
    }


    // Start is called before the first frame update
    protected void Start()
    {
        //get Rigidbody
        _rb = GetComponent<Rigidbody>();

        _explosionPrefab = Resources.Load<GameObject>("Prefabs/SpaceShip Explosion");

        StoreShipDimensions();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary")) return;
        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.ShipDestroyed, new EventArgs());
    }

    public float GetWidth()
    {
        if (_shipHeight > 0) return _shipHeight;
        else
        {
            StoreShipDimensions();
        }

        return _shipWidth;
    }

    public float GetHeight()
    {
        if (_shipWidth > 0) return _shipWidth;
        else
        {
            StoreShipDimensions();
        }

        return _shipHeight;
    }

    protected virtual void StoreShipDimensions()
    {
        // set ship width and height using collider
        SphereCollider collider = GetComponent<SphereCollider>();
        _shipWidth = collider.radius * 2 * transform.localScale.x;
        _shipHeight = collider.radius * 2 * transform.localScale.z;
    }

}
