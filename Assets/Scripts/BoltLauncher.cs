using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltLauncher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Spawn a Bolt on the location of the game object
    public void LaunchBolt(GameObject boltPrefab)
    {
        Instantiate(boltPrefab, transform.position, Quaternion.identity);
    }
}
