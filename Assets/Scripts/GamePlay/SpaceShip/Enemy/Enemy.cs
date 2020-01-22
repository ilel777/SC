using UnityEngine;

public class Enemy : SpaceShip
{
    #region Fields

    // Support Movement
    private EnemyMovement _movement;

    // Support Attack
    private EnemyAttack _attack;

    #endregion


    #region Properties
    public new EnemyShipConfig DefaultConfig { get => base.DefaultConfig as EnemyShipConfig; }
    #endregion

    new void Awake()
    {
        base.Awake();

        // initialize movement component
        _movement = gameObject.AddComponent<EnemyMovement>();

        // initialize attack component
        _attack = gameObject.AddComponent<EnemyAttack>();

    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        // configure attack component
        _attack.Power = DefaultConfig.attack.power;
        _attack.FireRate = 1 / DefaultConfig.attack.cooldown;
        _attack.BoltThrustForce = DefaultBoltConfig.movement.speed;
        _attack.Bolts = PoolsContainer.BoltPools[DefaultBoltConfig.name];

        // configure health component
        Health.LifePoints = DefaultConfig.health.lifePoints;


        transform.Rotate(Vector3.up, Mathf.PI * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        // make sure the bolt is out of screen
        if (transform.position.magnitude > (new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenTop)).magnitude * 2)
        {
            PoolsContainer.SpaceShipPools[name].Return(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary")
            || other.gameObject.CompareTag("Enemy Bolt")
            || other.gameObject.CompareTag("Powerup")) return;

        gameObject.SetActive(false);
        PoolsContainer.SpaceShipPools[name].Return(gameObject);
        EventManager.TriggerEvent(EventName.EnemyShipDestroyed, new EnemyShipDestroyedEventArgs(DefaultConfig.scoreValue));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health.TakeDamage((uint)collision.gameObject.GetComponent<Attack>().Power);
        if (Health.IsDestroyed)
        {
            PoolsContainer.SpaceShipPools[name].Return(gameObject);
            GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 3.0f);
            EventManager.TriggerEvent(EventName.EnemyShipDestroyed, new EnemyShipDestroyedEventArgs(DefaultConfig.scoreValue));
        }
    }
}
