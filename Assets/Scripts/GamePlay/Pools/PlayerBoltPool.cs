using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoltPool : BoltPool
{
    public PlayerBoltPool(GameObject boltPrefab, int initialCapacity = 10) : base(boltPrefab, initialCapacity)
    {
    }

    protected override GameObject CreateNewObject()
    {
        GameObject bolt = base.CreateNewObject();
        bolt.AddComponent<PlayerBolt>();
        return bolt;
    }
}
