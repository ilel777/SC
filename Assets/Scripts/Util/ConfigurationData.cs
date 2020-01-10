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
        playerShipConfig.speed = 80;
        playerShipConfig.cooldown = 0.5f;
        playerShipConfig.power = 200;

        // set default values for enemy config
        enemyShipConfig.health = 50;
        enemyShipConfig.speed = 40;
        enemyShipConfig.scoreValue = 150;
        enemyShipConfig.cooldown = 1.0f;
        enemyShipConfig.power = 70;

        // set default values for asteroid config
        asteroidConfig.health = 100;
        asteroidConfig.speed = 40;
        asteroidConfig.rotationSpeed = 0.25f;
        asteroidConfig.scoreValue = 100;
        asteroidConfig.power = 1000;

        // set default values for collectible config
        collectibleConfig.speed = 55;

        // set default values for player bolt config
        playerBoltConfig.impulseForce = 70;
        playerBoltConfig.power = 50;

        // set default values for enemy bolt config
        enemyBoltConfig.impulseForce = 60;
        enemyBoltConfig.power = 30;
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
    public uint health;
    public float cooldown;
    public uint power;
}

[System.Serializable]
public struct EnemyShipConfig
{
    public float speed;
    public uint health;
    public float cooldown;
    public uint power;
    public int scoreValue;
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
    public uint health;
    public uint power;
    public float rotationSpeed;
    public int scoreValue;
}

[System.Serializable]
public struct PlayerBoltConfig
{
    public float impulseForce;
    public uint power;
}

[System.Serializable]
public struct EnemyBoltConfig
{
    public float impulseForce;
    public uint power;
}
