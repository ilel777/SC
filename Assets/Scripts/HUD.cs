using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Support displaying level stats
    [SerializeField]
    private Text playerScore, enemyShipsDestroyed, asteroidsDestroyed, livesLeft, powerupsCollected;

    // Store a reference to LevelStat object containing level statistics
    private LevelStat levelStat;
    // Start is called before the first frame update
    void Start()
    {
        levelStat = GameObject.FindObjectOfType<LevelStat>();
    }

    // Update is called once per frame
    void Update()
    {
        playerScore.text = "Score: " + levelStat.Score;
        enemyShipsDestroyed.text = "Enemy Ships Destroyed: " + levelStat.EnemyShipsDestroyed;
        asteroidsDestroyed.text = "Asteroids Destroyed: " + levelStat.AsteroidsDestroyed;
        livesLeft.text = "Lives Left: " + levelStat.PlayerLives;
        powerupsCollected.text = "Powerups Collected: " + levelStat.PowerupsCollected;
    }

    //     void OnEnable()
    //     {
    //         EventManager.StartListening(EventName.ScoreChanged, HandleScoreChangedEvent);
    //     }

    //     void OnDesable()
    //     {
    //         EventManager.StopListening(EventName.ScoreChanged, HandleScoreChangedEvent);

    //     }

    //     void HandleScoreChangedEvent(EventArgs e)
    //     {
    //         playerScore.text = "Score: " + (e as ScoreChangedEventArgs).Score;
    //     }
}
