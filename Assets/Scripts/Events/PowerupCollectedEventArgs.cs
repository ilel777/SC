using System;

internal class PowerupCollectedEventArgs : EventArgs
{
    private Powerup _powerup;

    public PowerupCollectedEventArgs(Powerup powerup)
    {
        this._powerup = powerup;
    }

    public Powerup Powerup { get => _powerup; }
}
