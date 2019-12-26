using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltLauncher : MonoBehaviour
{
    SpaceShip _ship;

    void Start()
    {
        _ship = GetComponentInParent<SpaceShip>();
    }
    // Spawn a Bolt on the location of the game object
    public void LaunchBolt()
    {
        _ship.Bolt.transform.Rotate(new Vector3(0, 0, Vector3.Angle(_ship.Bolt.transform.up, transform.forward)));
        GameObject bolt = Instantiate(_ship.Bolt, transform.position, _ship.Bolt.transform.rotation);
        bolt.SetActive(true);
        bolt.GetComponent<Rigidbody>().AddForce(transform.forward * _ship.BoltThrustForce * bolt.GetComponent<Rigidbody>().mass, ForceMode.Impulse);
    }
}
