using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigurationUtils
{
    private static ConfigurationData configurationData;

    // public static PlayerShipConfig PlayerShipConfig { get => configurationData.playerShipConfig; }
    // public static EnemyShipConfig EnemyShipConfig { get => configurationData.enemyShipConfig; }
    public static SpaceShipConfig[] SpaceShipsConfig { get => configurationData.spaceShipsConfig; }
    // public static AsteroidConfig AsteroidConfig { get => configurationData.asteroidConfig; }
    public static ObstacleConfig[] ObstaclesConfig { get => configurationData.obstaclesConfig; }
    public static CollectibleConfig[] CollectiblesConfig { get => configurationData.collectiblesConfig; }
    // public static BoltConfig PlayerBoltConfig { get => configurationData.playerBoltConfig; }
    // public static BoltConfig EnemyBoltConfig { get => configurationData.enemyBoltConfig; }
    public static WaveConfig WaveConfig { get => configurationData.waveConfig; }

    public static void Initialize()
    {
        configurationData = ConfigurationData.getConfigurationData();
    }
}
