using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigurationUtils
{
    private static ConfigurationData configurationData;

    public static PlayerShipConfig PlayerShipConfig { get => configurationData.playerShipConfig; }
    public static EnemyShipConfig EnemyShipConfig { get => configurationData.enemyShipConfig; }
    public static AsteroidConfig AsteroidConfig { get => configurationData.asteroidConfig; }
    public static CollectibleConfig CollectibleConfig { get => configurationData.collectibleConfig; }
    public static PlayerBoltConfig PlayerBoltConfig { get => configurationData.playerBoltConfig; }
    public static EnemyBoltConfig EnemyBoltConfig { get => configurationData.enemyBoltConfig; }
    public static WaveConfig WaveConfig { get => configurationData.waveConfig; }
    public static void Initialize()
    {
        configurationData = ConfigurationData.getConfigurationData();
    }
}
