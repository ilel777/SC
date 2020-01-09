using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsContainer
{
    #region Fields

    private static PoolsContainer _instance;
    private PlayerBoltPool _playerBolts;
    private EnemyBoltPool _enemyBolts;
    private EnemyPool _enemies;
    private AsteroidPool _asteroids;
    private PowerupPool _powerups;

    #endregion

    #region Accessors

    public static PlayerBoltPool PlayerBolts { get => _instance._playerBolts; }
    public static EnemyPool Enemies { get => _instance._enemies; }
    public static EnemyBoltPool EnemyBolts { get => _instance._enemyBolts; }
    public static AsteroidPool Asteroids { get => _instance._asteroids; }
    public static PowerupPool Powerups { get => _instance._powerups; }

    #endregion

    #region Methods
    public static void Initialize()
    {
        if (_instance != null) return;
        _instance = new PoolsContainer();
    }

    #endregion

    #region Constructors

    PoolsContainer()
    {
        _playerBolts = new PlayerBoltPool(Resources.Load<GameObject>("Prefabs/Bolt"), 100);
        _enemyBolts = new EnemyBoltPool(Resources.Load<GameObject>("Prefabs/Bolt"), 100);
        _enemies = new EnemyPool(Resources.Load<GameObject>("Prefabs/SpaceItems/Enemy"), 10);
        _asteroids = new AsteroidPool(Resources.Load<GameObject>("Prefabs/SpaceItems/Asteroid"), 10);
        _powerups = new PowerupPool(Resources.Load<GameObject>("Prefabs/SpaceItems/Health Powerup"), 20);
    }

    #endregion
}
