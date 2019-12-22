using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Component waveManager;
    private int score;

    void Awake()
    {
        gameObject.AddComponent<GameInitializer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waveManager = gameObject.AddComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        EventManager.StartListening(EventName.AsteroidDestroyed, HandleAsteroidDestroyedEvent);
    }

    private void HandleAsteroidDestroyedEvent(EventArgs args)
    {
        score += (args as AsteroidDestroyedEventArgs).ScoreValue;
        EventManager.TriggerEvent(EventName.ScoreChanged, new ScoreChangedEventArgs(score));
    }

    void OnDisable()
    {

    }
}
