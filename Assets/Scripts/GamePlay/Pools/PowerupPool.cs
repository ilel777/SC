using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPool : GameObjectPool
{
    #region Constructors

    public PowerupPool(GameObject powerupPrefab, int initialCapacity = 10) : base(powerupPrefab, initialCapacity)
    {
        PoolGameObject.name = "Powerup Pool";
    }

    public PowerupPool(GameObject prefab, string name, int initialCapacity = 10) : base(prefab, name, initialCapacity)
    {
        PoolGameObject.name = "Powerup Pool";
    }
    #endregion
}
