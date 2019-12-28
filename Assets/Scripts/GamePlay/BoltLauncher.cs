using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltLauncher : MonoBehaviour
{
    // holds SpaceShip reference
    SpaceShip _ship;

    // Support cooldown
    Timer _cooldown;

    // get if the cannon is ready to fire
    public bool ReadyToFire { get => _cooldown.Finished || !_cooldown.Running; }

    void Start()
    {
        _ship = GetComponentInParent<SpaceShip>();
        _cooldown = gameObject.AddComponent<Timer>();
    }

    // Spawn a Bolt on the location of the game object
    public void LaunchBolt()
    {
        _ship.Bolt.transform.Rotate(new Vector3(0, 0, Vector3.Angle(_ship.Bolt.transform.up, transform.forward)));
        GameObject bolt = Instantiate(_ship.Bolt, transform.position, _ship.Bolt.transform.rotation);
        bolt.SetActive(true);
        bolt.GetComponent<Rigidbody>().AddForce(transform.forward * _ship.BoltThrustForce * bolt.GetComponent<Rigidbody>().mass, ForceMode.Impulse);

        // Start CooldownTimer
        _cooldown.Duration = 1 / (_ship.FireRate / 2);
        _cooldown.Run();
    }
}
