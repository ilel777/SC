using UnityEngine;

public class GameObjectPool : Pool<GameObject>
{
    GameObject _prefab;
    GameObject _poolGameObject;
    GameObjectConfig _config;

    public GameObjectPool(GameObjectConfig config, int initialCapacity = 10) : base(initialCapacity)
    {
        _config = config;
        _prefab = Resources.Load<GameObject>("Prefabs/" + config.prefabPath);
        _poolGameObject = new GameObject();
        _poolGameObject.transform.position = Vector3.zero;
        _poolGameObject.name = _config.name + " Pool";
        // Object.DontDestroyOnLoad(_poolGameObject);
        for (int i = 0; i < initialCapacity; i++)
        {
            _items.Add(CreateNewObject());
        }
    }

    public GameObject PoolGameObject { get => _poolGameObject; }

    protected override GameObject CreateNewObject()
    {
        GameObject item = GameObject.Instantiate(_prefab, _poolGameObject.transform);
        OnCreate(item);
        item.SetActive(false);
        return item;
    }

    protected override void OnCreate(GameObject item)
    {
        item.name = _config.name;
        item.GetComponent<IConfig>().DefaultConfig = _config;
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
