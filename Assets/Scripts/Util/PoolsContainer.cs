using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolsContainer
{
    #region Fields

    private static PoolsContainer _instance;
    private PlayerBoltPool _playerBolts;
    private EnemyBoltPool _enemyBolts;
    private GameObjectPool _enemies;
    private GameObjectPool _asteroids;
    private Dictionary<String, GameObjectPool> _powerupPools;


    #endregion

    #region Accessors
    public static PlayerBoltPool PlayerBolts { get => _instance._playerBolts; }
    public static GameObjectPool Enemies { get => _instance._enemies; }
    public static EnemyBoltPool EnemyBolts { get => _instance._enemyBolts; }
    public static GameObjectPool Asteroids { get => _instance._asteroids; }
    public static Dictionary<String, GameObjectPool> PowerupPools { get => _instance._powerupPools; }

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
        _playerBolts = new PlayerBoltPool(ConfigurationUtils.PlayerBoltConfig, 100);
        _enemyBolts = new EnemyBoltPool(ConfigurationUtils.EnemyBoltConfig, 100);
        _enemies = new GameObjectPool(ConfigurationUtils.EnemyShipConfig, 10);
        _asteroids = new GameObjectPool(ConfigurationUtils.AsteroidConfig, 10);
        // _powerups = new PowerupPool(Resources.Load<GameObject>("Prefabs/SpaceItems/Health Powerup"), 20);
        _powerupPools = new Dictionary<string, GameObjectPool>();
        foreach (CollectibleConfig config in ConfigurationUtils.CollectiblesConfig)
        {
            _powerupPools.Add(config.name, new GameObjectPool(config, 10));
        }
    }

    #endregion
}
