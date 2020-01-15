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
    private Health _playerHealth;


    #endregion

    #region Properties

    public int AsteroidsDestroyed { get => _asteroidsDestroyed; set => _asteroidsDestroyed = value; }
    public int Score { get => _score; set => _score = value; }
    public int EnemyShipsDestroyed { get => _enemyShipsDestroyed; set => _enemyShipsDestroyed = value; }
    public int PlayerLives { get => _playerLives; set => _playerLives = value; }
    public int PowerupsCollected { get => _powerupsCollected; set => _powerupsCollected = value; }
    public Health PlayerHealth { get => _playerHealth; }

    #endregion

    #region Methods

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        _playerLives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerHealth) _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    public void UpdateScore(int scoreValue)
    {
        Score += scoreValue;
        EventManager.TriggerEvent(EventName.ScoreChanged, new ScoreChangedEventArgs(_score));
    }

    #endregion

}
