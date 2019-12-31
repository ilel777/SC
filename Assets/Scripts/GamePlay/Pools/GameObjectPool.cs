using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : Pool<GameObject>
{
    GameObject _prefab;
    public GameObjectPool(GameObject prefab, int initialCapacity = 10) : base(initialCapacity)
    {
        _prefab = prefab;
    }

    protected override GameObject CreateNewObject()
    {
        return GameObject.Instantiate(_prefab);
    }

    protected override void OnReturn(GameObject item)
    {
        item.GetComponent<Rigidbody>().velocity = Vector3.zero;
        item.SetActive(false);
    }
}
