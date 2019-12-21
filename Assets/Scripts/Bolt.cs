using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    // Support movement
    private Rigidbody rb;
    private float shotForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * rb.mass * ConfigurationUtils.PlayerBoltConfig.impulseForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
