using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    #region Fields

    private float _speed;

    #endregion


    #region Properties

    public float Speed { get => _speed; set => _speed = value; }

    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();
}
