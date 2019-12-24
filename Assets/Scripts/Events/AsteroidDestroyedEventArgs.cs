using System;

public class AsteroidDestroyedEventArgs: EventArgs
{
    int _scoreValue;
    public AsteroidDestroyedEventArgs(int scoreValue){
        _scoreValue = scoreValue;
    }

    public int ScoreValue { get => _scoreValue; }
}
