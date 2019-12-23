using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Support wave management
    WaveManager waveManager;

    // Support storing level statistics
    LevelStat levelStat;

    // Support storing player prefab for later instantiation
    GameObject playerPrefab;

    void Awake()
    {
        gameObject.AddComponent<GameInitializer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waveManager = gameObject.AddComponent<WaveManager>();
        levelStat = gameObject.AddComponent<LevelStat>();
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        EventManager.StartListening(EventName.AsteroidDestroyed, HandleAsteroidDestroyedEvent);
        EventManager.StartListening(EventName.PlayerDestroyed, HandlePlayerDestroyedEvent);
    }

    /// <summary>
    ///   Pauses the game and Instantiate player again if still has Lives left
    ///   or Trigger game over event
    /// </summary>
    private void HandlePlayerDestroyedEvent(EventArgs arg0)
    {
        if (levelStat.PlayerLives > 0)
        {
            levelStat.PlayerLives--;
            GameObject player = Instantiate(playerPrefab);
        }
        else
        {
            EventManager.TriggerEvent(EventName.GameOver, new EventArgs());
        }
    }

    /// <summary>
    ///   use LevelStat object to update statistics
    /// </summary>
    private void HandleAsteroidDestroyedEvent(EventArgs args)
    {
        levelStat.UpdateScore((args as AsteroidDestroyedEventArgs).ScoreValue);
        levelStat.AsteroidsDestroyed++;
    }

    void OnDisable()
    {
        EventManager.StopListening(EventName.AsteroidDestroyed, HandleAsteroidDestroyedEvent);
    }
}
