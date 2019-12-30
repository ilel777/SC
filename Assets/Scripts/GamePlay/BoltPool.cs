using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPool : Pool<GameObject>
{
    GameObject _boltPrefab;

    #region Constructors

    public BoltPool(int initialCapacity) : base(initialCapacity)
    {
        _boltPrefab = Resources.Load<GameObject>("Prefabs/Bolt");
    }

    #endregion

    #region Methods

    protected override GameObject CreateNewObject()
    {
        GameObject bolt = GameObject.Instantiate(_boltPrefab);
        bolt.SetActive(false);
        return bolt;
    }

    protected override void OnReturn(GameObject item)
    {
        item.GetComponent<Rigidbody>().velocity = Vector3.zero;
        item.SetActive(false);
    }

    #endregion
}
