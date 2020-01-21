using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoltPool : GameObjectPool
{
    public PlayerBoltPool(BoltConfig config, int initialCapacity = 10) : base(config, initialCapacity)
    {
    }

    //     protected override GameObject CreateNewObject()
    //     {
    //         GameObject bolt = base.CreateNewObject();
    //         bolt.AddComponent<PlayerBolt>();
    //         return bolt;
    //     }

    protected override void OnCreate(GameObject item)
    {
        item.AddComponent<PlayerBolt>();
        base.OnCreate(item);
    }
}
