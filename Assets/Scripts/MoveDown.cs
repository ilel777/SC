using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   MoveDown
/// </summary>
public class MoveDown : MonoBehaviour
{

    // support moving down
    [SerializeField]
    private float speed = 10;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.back * speed;
        // Debug.Log(Vector3.back);
        // Debug.Log(Vector3.back * speed * Time.deltaTime);
    }
}
