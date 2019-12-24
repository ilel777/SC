using System;

public class PowerupDurationEndedEventArgs : EventArgs
{
    Powerup _powerup;

    #region Ctor & Destructor
    /// <summary>
    /// Standard Constructor
    /// </summary>
    public PowerupDurationEndedEventArgs(Powerup powerup)
    {
        _powerup = powerup;
    }

    #endregion

    public Powerup Powerup { get => _powerup; }

}
