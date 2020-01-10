using System;
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
    public bool IsDestroyed { get => _lifePoints == 0; }

    #endregion

    #region Methods

    public void Recover(uint hp)
    {
        _lifePoints += hp;
    }

    public void TakeDamage(uint attackPower)
    {
        if (attackPower > _lifePoints) _lifePoints = 0;
        else
            _lifePoints -= attackPower;
    }

    #endregion
}
