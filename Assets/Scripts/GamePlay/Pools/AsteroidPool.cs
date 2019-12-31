using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : GameObjectPool
{
    public AsteroidPool(GameObject asteroidPrefab, int initialCapacity = 10) : base(asteroidPrefab, initialCapacity)
    {
    }
}
