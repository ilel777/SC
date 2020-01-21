using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : Pool<GameObject>
{
    GameObject _prefab;
    GameObject _poolGameObject;
    string _name;

    public GameObjectPool(GameObject prefab, int initialCapacity = 10) : this(prefab, "any name", initialCapacity) { }

    public GameObjectPool(GameObject prefab, string name, int initialCapacity = 10) : base(initialCapacity)
    {
        _prefab = prefab;
        _name = name;
        _poolGameObject = new GameObject();
        _poolGameObject.transform.position = Vector3.zero;
        _poolGameObject.name = "Pool Game Object";
        // Object.DontDestroyOnLoad(_poolGameObject);
        for (int i = 0; i < 10; i++)
        {
            _items.Add(CreateNewObject());
        }
    }

    public GameObject PoolGameObject { get => _poolGameObject; }

    protected override GameObject CreateNewObject()
    {
        GameObject item = GameObject.Instantiate(_prefab, _poolGameObject.transform);
        item.name = _name;
        item.SetActive(false);
        return item;
    }

    protected override GameObject OnGet(GameObject item)
    {
        item.SetActive(true);
        // item.transform.SetParent(_poolGameObject.transform.parent, true);
        return item;
    }

    protected override void OnReturn(GameObject item)
    {
        item.GetComponent<Rigidbody>().velocity = Vector3.zero;
        // item.transform.position = Vector3.zero;
        // item.transform.SetParent(_poolGameObject.transform, true);
        item.SetActive(false);
    }
}
