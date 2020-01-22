using UnityEngine;

public class PlayerBolt : Bolt
{

    public new BoltConfig DefaultConfig { get => base.DefaultConfig as BoltConfig; }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        GetComponent<MeshRenderer>().material = new Material(Resources.Load<Material>("Materials/PlayerBolt"));
        tag = "Player Bolt";
        gameObject.layer = 9;

        Attack.Power = DefaultConfig.attack.power;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Powerup")) return;
        PoolsContainer.BoltPools[name].Return(gameObject);
        // Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        PoolsContainer.BoltPools[name].Return(gameObject);
        Destroy(Instantiate(ExplosionPrefab, transform.position, Quaternion.identity), 3.0f);
    }

}
