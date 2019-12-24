using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStat : MonoBehaviour
{
    #region Fields

    private int _score;
    private int _enemyShipsDestroyed;
    private int _playerLives;
    private int _asteroidsDestroyed;
    private int _powerupsCollected;


    #endregion

    #region Properties

    public int AsteroidsDestroyed { get => _asteroidsDestroyed; set => _asteroidsDestroyed = value; }
    public int Score { get => _score; set => _score = value; }
    public int EnemyShipsDestroyed { get => _enemyShipsDestroyed; set => _enemyShipsDestroyed = value; }
    public int PlayerLives { get => _playerLives; set => _playerLives = value; }
    public int PowerupsCollected { get => _powerupsCollected; set => _powerupsCollected = value; }

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        _playerLives = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int scoreValue)
    {
        Score += scoreValue;
        EventManager.TriggerEvent(EventName.ScoreChanged, new ScoreChangedEventArgs(_score));
    }

    #endregion

}
