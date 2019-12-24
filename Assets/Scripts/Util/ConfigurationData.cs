using System;
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
    public CollectibleConfig collectibleConfig;
    public PlayerBoltConfig playerBoltConfig;
    public EnemyBoltConfig enemyBoltConfig;

    private ConfigurationData()
    {
        // set default values for player configs
        playerShipConfig.health = 100;
        playerShipConfig.speed = 20;
        playerShipConfig.cooldown = 0.5f;

        // set default values for enemy config
        enemyShipConfig.health = 50;
        enemyShipConfig.speed = 10;
        enemyShipConfig.scoreValue = 150;
        enemyShipConfig.cooldown = 1.0f;

        // set default values for asteroid config
        asteroidConfig.speed = 10;
        asteroidConfig.rotationSpeed = 0.25f;
        asteroidConfig.scoreValue = 100;

        // set default values for collectible config
        collectibleConfig.speed = 15;

        // set default values for player bolt config
        playerBoltConfig.impulseForce = 17;

        // set default values for enemy bolt config
        enemyBoltConfig.impulseForce = 15;
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
public struct PlayerShipConfig
{
    public float speed;
    public int health;
    public float cooldown;
}

[System.Serializable]
public struct EnemyShipConfig
{
    public float speed;
    public int health;
    public int scoreValue;
    public float cooldown;
}

[System.Serializable]
public struct CollectibleConfig
{
    public float speed;
}

[System.Serializable]
public struct AsteroidConfig
{
    public float speed;
    public float rotationSpeed;
    public int scoreValue;
}

[System.Serializable]
public struct PlayerBoltConfig
{
    public float impulseForce;
}

[System.Serializable]
public struct EnemyBoltConfig
{
    public float impulseForce;
}
