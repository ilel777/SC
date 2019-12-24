using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    // Support storing level statistics
    LevelStat _levelStatistics;

    // Support storing player prefab for later instantiation
    GameObject _playerPrefab;

    public LevelStat LevelStatistics { get => _levelStatistics; }

    // Start is called before the first frame update
    void Start()
    {
        _levelStatistics = gameObject.AddComponent<LevelStat>();
        _playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
    }


    void OnEnable()
    {
        EventManager.StartListening(EventName.AsteroidDestroyed, HandleAsteroidDestroyedEvent);
        EventManager.StartListening(EventName.PlayerDestroyed, HandlePlayerDestroyedEvent);
        EventManager.StartListening(EventName.EnemyShipDestroyed, HandleEnemyShipDestroyedEvent);
        EventManager.StartListening(EventName.PowerupCollected, HandlePowerupCollectedEvent);
    }


    private void HandlePowerupCollectedEvent(EventArgs arg)
    {
        _levelStatistics.PowerupsCollected++;
    }

    /// <summary>
    ///   Use LevelStat object to update statistics
    /// </summary>
    private void HandleEnemyShipDestroyedEvent(EventArgs arg)
    {
        _levelStatistics.UpdateScore((arg as EnemyShipDestroyedEventArgs).ScoreValue);
        _levelStatistics.EnemyShipsDestroyed++;
    }

    /// <summary>
    ///   Pauses the game and Instantiate player again if still has Lives left
    ///   or Trigger game over event
    /// </summary>
    private void HandlePlayerDestroyedEvent(EventArgs arg0)
    {
        if (_levelStatistics.PlayerLives > 0)
        {
            _levelStatistics.PlayerLives--;
            GameObject player = Instantiate(_playerPrefab);
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
        _levelStatistics.UpdateScore((args as AsteroidDestroyedEventArgs).ScoreValue);
        _levelStatistics.AsteroidsDestroyed++;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameOver())
        {
            EventManager.TriggerEvent(EventName.GameOver, new EventArgs());
        }
        else if (MissionComplete())
        {
            EventManager.TriggerEvent(EventName.MissionComplete, new EventArgs());
        }
    }

    void OnDisable()
    {
        EventManager.StopListening(EventName.AsteroidDestroyed, HandleAsteroidDestroyedEvent);
        EventManager.StopListening(EventName.PlayerDestroyed, HandlePlayerDestroyedEvent);
        EventManager.StopListening(EventName.EnemyShipDestroyed, HandleEnemyShipDestroyedEvent);
        EventManager.StopListening(EventName.PowerupCollected, HandlePowerupCollectedEvent);
    }

    public abstract bool MissionComplete();
    public abstract bool GameOver();
}
