using System;

public class EnemyShipDestroyedEventArgs : EventArgs
{
    int _scoreValue;

    public EnemyShipDestroyedEventArgs(int scoreValue)
    {
        _scoreValue = scoreValue;
    }

    public int ScoreValue { get => _scoreValue; }
}
