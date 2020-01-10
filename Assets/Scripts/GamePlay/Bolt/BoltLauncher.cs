using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltLauncher : MonoBehaviour
{
    // holds SpaceShip reference
    SpaceShipAttack _shipAttack;

    // Support cooldown
    Timer _cooldown;

    // get if the cannon is ready to fire
    public bool ReadyToFire { get => _cooldown.Finished || !_cooldown.Running; }

    void Start()
    {
        _shipAttack = GetComponentInParent<SpaceShipAttack>();
        _cooldown = gameObject.AddComponent<Timer>();
    }

    // Spawn a Bolt on the location of the game object
    public void LaunchBolt()
    {
        GameObject bolt = _shipAttack.PrepareNewBolt();
        bolt.transform.Rotate(new Vector3(0, 0, Vector3.Angle(bolt.transform.up, transform.forward)));
        bolt.transform.position = transform.position;
        bolt.SetActive(true);
        bolt.GetComponent<Rigidbody>().AddForce(transform.forward * _shipAttack.BoltThrustForce * bolt.GetComponent<Rigidbody>().mass, ForceMode.Impulse);

        // Start CooldownTimer
        _cooldown.Duration = 1 / (_shipAttack.FireRate / 2);
        _cooldown.Run();
    }
}
