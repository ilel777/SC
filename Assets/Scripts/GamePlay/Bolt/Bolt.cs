using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bolt : MonoBehaviour
{
    // Support movement
    private Rigidbody _rb;

    private Attack _attack;

    // give fields access to child classes
    public Rigidbody Rb { get => _rb; }
    public Attack Attack { get => _attack; set => _attack = value; }

    protected void Awake()
    {
        Attack = gameObject.AddComponent<Attack>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        // make sure the bolt is out of screen
        if (transform.position.magnitude > (new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenTop)).magnitude * 2)
        {
            if (gameObject.GetComponent<PlayerBolt>())
            {
                PoolsContainer.PlayerBolts.Return(gameObject);
            }
            else if (gameObject.GetComponent<EnemyBolt>())
            {
                PoolsContainer.EnemyBolts.Return(gameObject);
            }
        }
    }

}
