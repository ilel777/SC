using System.IO;
using UnityEngine;

[System.Serializable]
public class ConfigurationData
{
    public LevelConfig[] levelsConfig;

    private ConfigurationData()
    {
        // set default values for level configs
        levelsConfig = new LevelConfig[2];
        LevelConfig levelConfig = new LevelConfig("Level01");

        // set default values for spaceships configs
        SpaceShipConfig[] spaceShipsConfig = new SpaceShipConfig[2];

        // set default values for player configs
        PlayerShipConfig playerShipConfig = new PlayerShipConfig("Player", "Player");
        playerShipConfig.health = new HealthConfig(100);
        playerShipConfig.movement = new MovementConfig(80);
        playerShipConfig.attack = new AttackConfig(200, 0.5f);
        // set default values for player bolt config
        BoltConfig playerBoltConfig = new BoltConfig("Player Bolt", "Bolt");
        playerBoltConfig.movement = new MovementConfig(100);
        playerBoltConfig.attack = new AttackConfig(50);
        playerShipConfig.boltConfig = playerBoltConfig;

        spaceShipsConfig.SetValue(playerShipConfig, 0);

        // set default values for enemy config
        EnemyShipConfig enemyShipConfig = new EnemyShipConfig("Enemy", "SpaceItems/Enemy");
        enemyShipConfig.health = new HealthConfig(50);
        enemyShipConfig.movement = new MovementConfig(60, 1.0f, 10.0f, 30.0f);
        enemyShipConfig.attack = new AttackConfig(70, 1.0f);
        enemyShipConfig.scoreValue = 150;
        // set default values for enemy bolt config
        BoltConfig enemyBoltConfig = new BoltConfig("Enemy Bolt", "Bolt");
        enemyBoltConfig.movement = new MovementConfig(90);
        enemyBoltConfig.attack = new AttackConfig(30);
        enemyShipConfig.boltConfig = enemyBoltConfig;

        spaceShipsConfig.SetValue(enemyShipConfig, 1);

        levelConfig.spaceShipsConfig = spaceShipsConfig;

        // set default values for obstacles
        ObstacleConfig[] obstaclesConfig = new ObstacleConfig[1];

        // set default values for asteroid config
        ObstacleConfig asteroidConfig = new ObstacleConfig("Red Asteroid", "SpaceItems/Red Asteroid");
        asteroidConfig.health = new HealthConfig(100);
        asteroidConfig.movement = new MovementConfig(60, 0.25f);
        asteroidConfig.scoreValue = 100;
        asteroidConfig.attack = new AttackConfig(1000);
        obstaclesConfig.SetValue(asteroidConfig, 0);

        levelConfig.obstaclesConfig = obstaclesConfig;

        // set default values for collectible config
        CollectibleConfig[] collectiblesConfig = new CollectibleConfig[2];
        // collectibleConfig.speed = 55;
        collectiblesConfig[0] = new CollectibleConfig("Health Powerup", "SpaceItems/Health Powerup");
        collectiblesConfig[0].movement = new MovementConfig(55);
        collectiblesConfig.SetValue(new CollectibleConfig("FireRate Powerup", "SpaceItems/FireRate Powerup"), 1);
        collectiblesConfig[1].movement = new MovementConfig(60);

        levelConfig.collectiblesConfig = collectiblesConfig;


        // set default values for wave config
        WaveConfig waveConfig = new WaveConfig();
        waveConfig.startWait = 0.5f;
        waveConfig.spawnWaveWait = 3.0f;
        waveConfig.spawnMessageDelay = 1.0f;
        waveConfig.spawnItemWait = 1.0f;
        waveConfig.itemsNumber = 5;

        levelConfig.waveConfig = waveConfig;
        levelsConfig.SetValue(levelConfig, 0);
    }

    public static ConfigurationData getConfigurationData()
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
public class LevelConfig
{
    public string name;
    public SpaceShipConfig[] spaceShipsConfig;
    public ObstacleConfig[] obstaclesConfig;
    public CollectibleConfig[] collectiblesConfig;
    public WaveConfig waveConfig;

    public LevelConfig(string name)
    {
        this.name = name;
    }
}

[System.Serializable]
public abstract class GameObjectConfig
{
    public string name;
    public string prefabPath;

    public GameObjectConfig(string name, string prefabPath) : this(name)
    {
        this.prefabPath = prefabPath;
    }

    protected GameObjectConfig(string name)
    {
        this.name = name;
    }
}

[System.Serializable]
public class ObstacleConfig : GameObjectConfig
{
    public MovementConfig movement;
    public HealthConfig health;
    public AttackConfig attack;
    public int scoreValue;

    public ObstacleConfig(string name, string prefabPath) : base(name, prefabPath)
    {
    }
}

[System.Serializable]
public class MovementConfig
{
    public float speed;
    public float rotationSpeed;
    public float dodgeCooldown;
    public float minDodgeForce;
    public float maxDodgeForce;

    public MovementConfig(float speed)
    {
        this.speed = speed;
    }

    public MovementConfig(float speed, float rotationSpeed) : this(speed)
    {
        this.rotationSpeed = rotationSpeed;
    }

    public MovementConfig(float speed,
                          float dodgeCooldown,
                          float minDodgeForce,
                          float maxDodgeForce) : this(speed)
    {
        this.dodgeCooldown = dodgeCooldown;
        this.minDodgeForce = minDodgeForce;
        this.maxDodgeForce = maxDodgeForce;
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
    public BoltConfig boltConfig;

    public SpaceShipConfig(string name, string prefabName) : base(name, prefabName)
    {
    }
}

[System.Serializable]
public class PlayerShipConfig : SpaceShipConfig
{

    public PlayerShipConfig(string name, string prefabName) : base(name, prefabName)
    {
    }
}

[System.Serializable]
public class EnemyShipConfig : SpaceShipConfig
{
    public int scoreValue;

    public EnemyShipConfig(string name, string prefabName) : base(name, prefabName)
    {
    }
}

[System.Serializable]
public class CollectibleConfig : GameObjectConfig
{
    public MovementConfig movement;

    public CollectibleConfig(string name, string prefabPath) : base(name, prefabPath)
    {
    }
}

// [System.Serializable]
// public class AsteroidConfig : GameObjectConfig
// {
//     public AsteroidConfig(string name, string prefabPath) : base(name, prefabPath)
//     {
//     }
// }

[System.Serializable]
public class BoltConfig : GameObjectConfig
{
    public MovementConfig movement;
    public AttackConfig attack;

    public BoltConfig(string name, string prefabName) : base(name, prefabName)
    {
    }
}

// [System.Serializable]
// public class PlayerBoltConfig : BoltConfig
// {
//     public PlayerBoltConfig(string name, string prefabName) : base(name, prefabName)
//     {
//     }
// }

// [System.Serializable]
// public class EnemyBoltConfig : BoltConfig
// {
//     public EnemyBoltConfig(string name, string prefabName) : base(name, prefabName)
//     {
//     }
// }

[System.Serializable]
public class WaveConfig
{
    public float spawnItemWait;
    public float spawnMessageDelay;
    public float startWait;
    public float spawnWaveWait;
    public int itemsNumber;
}
