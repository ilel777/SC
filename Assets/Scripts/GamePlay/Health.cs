using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Fields

    uint _lifePoints;

    #endregion

    #region Preperties

    public uint LifePoints { get => _lifePoints; set => _lifePoints = value; }

    #endregion

    #region Methods

    public void Recover(uint hp)
    {
        _lifePoints += hp;
    }

    public void Lose(uint hp)
    {
        _lifePoints -= hp;
    }

    #endregion
}
