using System;

public class ScoreChangedEventArgs : EventArgs
{
    private int _score;

    public ScoreChangedEventArgs(int score)
    {
        this._score = score;
    }

    public int Score { get => _score; }
}
