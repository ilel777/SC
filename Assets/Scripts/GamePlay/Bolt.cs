using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bolt : MonoBehaviour
{
    // Support movement
    private Rigidbody _rb;
    private float _shotForce;

    // give fields access to child classes
    public Rigidbody Rb { get => _rb; }
    public float ShotForce { get => _shotForce; set => _shotForce = value; }

    // Start is called before the first frame update
    protected void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
}
