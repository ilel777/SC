using System;
using System.Collections.Generic;

public class PoolsContainer
{
    #region Fields

    private static PoolsContainer _instance;
    // private PlayerBoltPool _playerBolts;
    // private EnemyBoltPool _enemyBolts;
    // private GameObjectPool _enemies;
    private Dictionary<string, GameObjectPool> _powerupPools;
    private Dictionary<string, GameObjectPool> _obstaclePools;
    private Dictionary<string, GameObjectPool> _spacesShipPools;
    private Dictionary<string, GameObjectPool> _boltPools;


    #endregion

    #region Accessors
    // public static PlayerBoltPool PlayerBolts { get => _instance._playerBolts; }
    // public static GameObjectPool Enemies { get => _instance._enemies; }
    // public static EnemyBoltPool EnemyBolts { get => _instance._enemyBolts; }
    public static Dictionary<string, GameObjectPool> PowerupPools { get => _instance._powerupPools; }
    public static Dictionary<string, GameObjectPool> ObstaclePools { get => _instance._obstaclePools; }
    public static Dictionary<string, GameObjectPool> SpaceShipPools { get => _instance._spacesShipPools; }
    public static Dictionary<string, GameObjectPool> BoltPools { get => _instance._boltPools; }

    #endregion

    #region Methods
    public static void Initialize()
    {
        if (_instance != null) return;
        _instance = new PoolsContainer();
    }

    public static void FreePools()
    {
        _instance = null;
    }

    #endregion

    #region Constructors

    PoolsContainer()
    {
        // _playerBolts = new PlayerBoltPool(ConfigurationUtils.PlayerShipConfig.boltConfig, 100);
        // _enemyBolts = new EnemyBoltPool(ConfigurationUtils.EnemyShipConfig.boltConfig, 100);
        // _enemies = new GameObjectPool(ConfigurationUtils.EnemyShipConfig, 10);

        _spacesShipPools = new Dictionary<string, GameObjectPool>();
        _boltPools = new Dictionary<string, GameObjectPool>();
        foreach (SpaceShipConfig config in ConfigurationUtils.SpaceShipsConfig)
        {
            if (config.name == "Player")
            {
                _spacesShipPools.Add(config.name, new GameObjectPool(config, 1));
                _boltPools.Add(config.boltConfig.name, new PlayerBoltPool(config.boltConfig, 10));
            }
            else
            {
                _spacesShipPools.Add(config.name, new GameObjectPool(config, 5));
                _boltPools.Add(config.boltConfig.name, new PlayerBoltPool(config.boltConfig, 10));
            }

        }

        // _asteroids = new GameObjectPool(ConfigurationUtils.AsteroidConfig, 10);
        _obstaclePools = new Dictionary<string, GameObjectPool>();
        foreach (ObstacleConfig config in ConfigurationUtils.ObstaclesConfig)
        {
            _obstaclePools.Add(config.name, new GameObjectPool(config, 10));
        }
        // _powerups = new PowerupPool(Resources.Load<GameObject>("Prefabs/SpaceItems/Health Powerup"), 20);
        _powerupPools = new Dictionary<string, GameObjectPool>();
        foreach (CollectibleConfig config in ConfigurationUtils.CollectiblesConfig)
        {
            _powerupPools.Add(config.name, new GameObjectPool(config, 10));
        }
    }

    #endregion
}
