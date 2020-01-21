using UnityEngine;

public class PlayerBolt : Bolt
{

    public new PlayerBoltConfig DefaultConfig { get => base.DefaultConfig as PlayerBoltConfig; }

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
        PoolsContainer.PlayerBolts.Return(gameObject);
        // Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        PoolsContainer.PlayerBolts.Return(gameObject);
        Destroy(Instantiate(ExplosionPrefab, transform.position, Quaternion.identity), 3.0f);
    }

}
