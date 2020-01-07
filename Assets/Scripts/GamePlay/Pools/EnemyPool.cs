using UnityEngine;

public class EnemyPool : GameObjectPool
{

    #region Constructors

    public EnemyPool(GameObject enemyPrefab, int initialCapacity = 10) : base(enemyPrefab, initialCapacity)
    {
        PoolGameObject.name = "Enemy Pool";
    }

    #endregion


    #region Methods

    #endregion
}
