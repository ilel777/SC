using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Support displaying player score
    [SerializeField]
    private Text playerScore;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        EventManager.StartListening(EventName.ScoreChanged, HandleScoreChangedEvent);
    }

    void OnDesable()
    {
        EventManager.StopListening(EventName.ScoreChanged, HandleScoreChangedEvent);

    }

    void HandleScoreChangedEvent(EventArgs e)
    {
        playerScore.text = "Score: " + (e as ScoreChangedEventArgs).Score;
    }
}
