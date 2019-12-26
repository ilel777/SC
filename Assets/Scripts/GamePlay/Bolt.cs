using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bolt : MonoBehaviour
{
    // Support movement
    private Rigidbody _rb;

    // give fields access to child classes
    public Rigidbody Rb { get => _rb; }

    // Start is called before the first frame update
    protected void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
}
