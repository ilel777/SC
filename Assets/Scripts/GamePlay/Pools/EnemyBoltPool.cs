using UnityEngine;

public class EnemyBoltPool : GameObjectPool
{
    public EnemyBoltPool(BoltConfig config, int initialCapacity = 10) : base(config, initialCapacity)
    {
    }

    //     protected override GameObject CreateNewObject()
    //     {
    //         GameObject bolt = base.CreateNewObject();
    //         bolt.AddComponent<EnemyBolt>();
    //         return bolt;
    //     }

    protected override void OnCreate(GameObject item)
    {
        item.AddComponent<EnemyBolt>();
        base.OnCreate(item);
    }
}
