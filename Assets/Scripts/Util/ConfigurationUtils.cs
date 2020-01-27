using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigurationUtils
{
    private static ConfigurationData configurationData;
    private static int _currentLevel;

    // public static PlayerShipConfig PlayerShipConfig { get => configurationData.playerShipConfig; }
    public static SpaceShipConfig[] SpaceShipsConfig { get => LevelConfig.spaceShipsConfig; }
    public static ObstacleConfig[] ObstaclesConfig { get => LevelConfig.obstaclesConfig; }
    public static CollectibleConfig[] CollectiblesConfig { get => LevelConfig.collectiblesConfig; }
    public static WaveConfig WaveConfig { get => LevelConfig.waveConfig; }
    public static LevelConfig LevelConfig { get => configurationData.levelsConfig[_currentLevel]; }


    public static void Initialize()
    {
        configurationData = GameObject.FindGameObjectWithTag("ConfigurationData").GetComponent<Configuration>().ConfigurationData;
        // configurationData = ConfigurationData.getConfigurationData();
    }

    public static void SetCurrentLevel(int currentLevel)
    {
        _currentLevel = currentLevel;
        Debug.Log(_currentLevel);
    }
}
