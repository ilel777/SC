using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    // Support storing level statistics
    LevelStat _levelStatistics;


    // Support managing Powerups
    List<Powerup> _powerups;

    // Support storing player prefab for later instantiation
    GameObject _playerPrefab;

    // Store player reference to apply effects
    Player _player;


    #region Properties

    public LevelStat LevelStatistics { get => _levelStatistics; }
    public Player Player { get => _player; set => _player = value; }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _levelStatistics = gameObject.AddComponent<LevelStat>();
        _playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        _player = GameObject.FindObjectOfType<Player>();
        _powerups = new List<Powerup>();
    }


    void OnEnable()
    {
        EventManager.StartListening(EventName.AsteroidDestroyed, HandleAsteroidDestroyedEvent);
        EventManager.StartListening(EventName.PlayerDestroyed, HandlePlayerDestroyedEvent);
        EventManager.StartListening(EventName.EnemyShipDestroyed, HandleEnemyShipDestroyedEvent);
        EventManager.StartListening(EventName.PowerupCollected, HandlePowerupCollectedEvent);
        EventManager.StartListening(EventName.PowerupDurationEnded, HandlePowerupDurationEnded);
    }


    private void HandlePowerupCollectedEvent(EventArgs arg)
    {
        _levelStatistics.PowerupsCollected++;
        Powerup powerup = (arg as PowerupCollectedEventArgs).Powerup;
        powerup.ApplyEffect(_player);
        powerup.gameObject.GetComponentInChildren<Renderer>().enabled = false;
        powerup.PowerupTimer = gameObject.AddComponent<Timer>();
        powerup.PowerupTimer.Duration = powerup.EffectDuration;
        powerup.PowerupTimer.Run();
        _powerups.Add(powerup);
    }

    private void HandlePowerupDurationEnded(EventArgs arg)
    {
        (arg as PowerupDurationEndedEventArgs).Powerup.DisableEffect(_player);
        PoolsContainer.Powerups.Return((arg as PowerupDurationEndedEventArgs).Powerup.gameObject);
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
            _player = Instantiate(_playerPrefab).GetComponent<Player>();
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
        for (int i = _powerups.Count - 1; i >= 0; i--)
        {
            if (_powerups[i].PowerupTimer.Finished)
            {
                EventManager.TriggerEvent(EventName.PowerupDurationEnded, new PowerupDurationEndedEventArgs(_powerups[i]));
                _powerups.RemoveAt(i);
            }
        }


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
