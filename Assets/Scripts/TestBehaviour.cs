using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            GameObject asteroid = PoolsContainer.Asteroids.Get();
            Debug.Log(asteroid.GetComponent<Movement>());
        }
    }
}
