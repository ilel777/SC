using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ConfigurationData
{
    public PlayerShipConfig playerShipConfig;
    public EnemyShipConfig enemyShipConfig;
    public AsteroidConfig asteroidConfig;
    public CollectibleConfig[] collectiblesConfig;
    public PlayerBoltConfig playerBoltConfig;
    public EnemyBoltConfig enemyBoltConfig;
    public WaveConfig waveConfig;

    private ConfigurationData()
    {
        // set default values for player configs
        playerShipConfig = new PlayerShipConfig("Player");
        playerShipConfig.health = new HealthConfig(100);
        playerShipConfig.movement = new MovementConfig(80);
        playerShipConfig.attack = new AttackConfig(200, 0.5f);

        // set default values for enemy config
        enemyShipConfig = new EnemyShipConfig("Enemy");
        enemyShipConfig.health = new HealthConfig(100);
        enemyShipConfig.movement = new MovementConfig(80);
        enemyShipConfig.attack = new AttackConfig(70, 1.0f);
        enemyShipConfig.scoreValue = 150;

        // set default values for asteroid config
        asteroidConfig = new AsteroidConfig("Asteroid");
        asteroidConfig.health = new HealthConfig(200);
        asteroidConfig.movement = new MovementConfig(80, 0.25f);
        asteroidConfig.scoreValue = 100;
        asteroidConfig.attack = new AttackConfig(1000);

        // set default values for collectible config
        collectiblesConfig = new CollectibleConfig[2];
        // collectibleConfig.speed = 55;
        collectiblesConfig[0] = new CollectibleConfig(55, "Health Powerup");
        collectiblesConfig.SetValue(new CollectibleConfig(60, "FireRate Powerup"), 1);

        // set default values for player bolt config
        playerBoltConfig = new PlayerBoltConfig("Player Bolt");
        playerBoltConfig.movement = new MovementConfig(100);
        playerBoltConfig.attack = new AttackConfig(50);

        // set default values for enemy bolt config
        enemyBoltConfig = new EnemyBoltConfig("Enemy Bolt");
        enemyBoltConfig.movement = new MovementConfig(90);
        enemyBoltConfig.attack = new AttackConfig(30);

        // set default values for wave config
        waveConfig = new WaveConfig();
        waveConfig.startWait = 0.5f;
        waveConfig.spawnWaveWait = 3.0f;
        waveConfig.spawnMessageDelay = 1.0f;
        waveConfig.spawnItemWait = 1.0f;
        waveConfig.itemsNumber = 5;
    }

    internal static ConfigurationData getConfigurationData()
    {
        ConfigurationData configData = new ConfigurationData();
        try
        {
            using (StreamReader sr = new StreamReader("Assets/Resources/Text/ConfigurationData.json"))
            {
                configData = JsonUtility.FromJson<ConfigurationData>(sr.ReadToEnd());
            }
        }
        catch
        {
            SaveConfigurationData();
        }

        return configData;
    }

    public static void SaveConfigurationData()
    {
        using (StreamWriter sw = new StreamWriter("Assets/Resources/Text/ConfigurationData.json"))
        {
            sw.Write(JsonUtility.ToJson(new ConfigurationData(), true));
        }
    }
}

[System.Serializable]
public abstract class GameObjectConfig
{
    public string name;
    public string prefabName;

    protected GameObjectConfig(string prefabName)
    {
        this.prefabName = prefabName;
    }
}

[System.Serializable]
public class MovementConfig
{
    public float speed;
    public float rotationSpeed;

    public MovementConfig(float speed)
    {
        this.speed = speed;
    }

    public MovementConfig(float speed, float rotationSpeed) : this(speed)
    {
        this.rotationSpeed = rotationSpeed;
    }
}
[System.Serializable]
public class AttackConfig
{
    public uint power;
    public float cooldown;
    private int v;

    public AttackConfig(uint power)
    {
        this.power = power;
    }

    public AttackConfig(uint power, float cooldown) : this(power)
    {
        this.cooldown = cooldown;
    }
}
[System.Serializable]
public class HealthConfig
{
    public uint lifePoints;

    public HealthConfig(uint lifePoints)
    {
        this.lifePoints = lifePoints;
    }
}

[System.Serializable]
public class SpaceShipConfig : GameObjectConfig
{
    public MovementConfig movement;
    public HealthConfig health;
    public AttackConfig attack;

    public SpaceShipConfig(string prefabName) : base(prefabName)
    {
    }
}

[System.Serializable]
public class PlayerShipConfig : SpaceShipConfig
{
    public PlayerShipConfig(string prefabName) : base(prefabName)
    {
    }
}

[System.Serializable]
public class EnemyShipConfig : SpaceShipConfig
{
    public int scoreValue;

    public EnemyShipConfig(string prefabName) : base(prefabName)
    {
    }
}

[System.Serializable]
public class CollectibleConfig : GameObjectConfig
{
    public MovementConfig movement;

    public CollectibleConfig(float speed, string prefabName) : base(prefabName)
    {
        this.movement = new MovementConfig(speed);
    }
}

[System.Serializable]
public class AsteroidConfig : GameObjectConfig
{
    public MovementConfig movement;
    public HealthConfig health;
    public AttackConfig attack;
    public int scoreValue;

    public AsteroidConfig(string prefabName) : base(prefabName)
    {
    }
}

[System.Serializable]
public class BoltConfig : GameObjectConfig
{
    public MovementConfig movement;
    public AttackConfig attack;

    public BoltConfig(string prefabName) : base(prefabName)
    {
    }
}

[System.Serializable]
public class PlayerBoltConfig : BoltConfig
{
    public PlayerBoltConfig(string prefabName) : base(prefabName)
    {
    }
}

[System.Serializable]
public class EnemyBoltConfig : BoltConfig
{
    public EnemyBoltConfig(string prefabName) : base(prefabName)
    {
    }
}

[System.Serializable]
public class WaveConfig
{
    public float spawnItemWait;
    public float spawnMessageDelay;
    public float startWait;
    public float spawnWaveWait;
    public int itemsNumber;
}
