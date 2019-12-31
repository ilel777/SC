using UnityEngine;

public class EnemyBoltPool : BoltPool
{
    public EnemyBoltPool(GameObject boltPrefab, int initialCapacity = 10) : base(boltPrefab, initialCapacity)
    {
    }

    protected override GameObject CreateNewObject()
    {
        GameObject bolt = base.CreateNewObject();
        bolt.AddComponent<EnemyBolt>();
        return bolt;
    }
}
