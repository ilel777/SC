using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpaceShip
{
    public override Timer CooldownTimer => throw new System.NotImplementedException();

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Boundary")) return;
        Destroy(gameObject);
        EventManager.TriggerEvent(EventName.EnemyShipDestroyed, new EnemyShipDestroyedEventArgs(ConfigurationUtils.EnemyShipConfig.scoreValue));
    }
}
