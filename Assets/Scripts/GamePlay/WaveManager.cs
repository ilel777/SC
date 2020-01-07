using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    float waveSpawnZ, waveSpawnXOffset;
    List<Pool<GameObject>> spaceObjectPools = new List<Pool<GameObject>>();

    int _objectSpawned;
    int _waveNumber;
    float _speedScale;

    // stores the max width and height of objects to be spawned
    float _maxWidth, _maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawnZ = ScreenUtils.ScreenTop + (ScreenUtils.ScreenTop / 5);
        waveSpawnXOffset = ScreenUtils.ScreenRight;
        // spaceObjects.AddRange(Resources.LoadAll<GameObject>("Prefabs/SpaceItems"));
        spaceObjectPools = new List<Pool<GameObject>>();
        spaceObjectPools.Add(PoolsContainer.Enemies);
        spaceObjectPools.Add(PoolsContainer.Asteroids);
        spaceObjectPools.Add(PoolsContainer.Powerups);

        foreach (Pool<GameObject> pool in spaceObjectPools)
        {
            GameObject obj = pool.Get();
            float width = obj.GetComponent<ISize>().GetWidth();
            float height = obj.GetComponent<ISize>().GetHeight();
            obj.SetActive(false);
            pool.Return(obj);

            if (_maxWidth < width)
            {
                _maxWidth = width;
            }
            if (_maxHeight < height)
            {
                _maxHeight = height;
            }
        }
        InvokeRepeating("SpawnWave", 1.0f, 1.0f);
    }

    void SpawnWave()
    {
        Pool<GameObject> pool = spaceObjectPools[Random.Range(0, spaceObjectPools.Count)];
        SpawnItem(pool.Get());
        if (++_objectSpawned == 5)
        {
            _waveNumber++;
            _speedScale = 0.1f * _waveNumber;
            Debug.Log("Movement Speed: " + _speedScale);
            _objectSpawned = 0;
        }
    }

    void SpawnItem(GameObject item)
    {
        Movement movement = item.GetComponent<Movement>();

        movement.Speed += (movement.Speed * _speedScale);

        Vector3 itemSpawnPostition = SpawnPosition(item.GetComponent<ISize>());
        if (itemSpawnPostition != Vector3.zero)
            item.transform.position = itemSpawnPostition;
        // Instantiate(item, itemSpawnPostition, Quaternion.identity);
    }

    /// <summary>
    ///   generate a random position inside left and right boundaries of the screen
    /// </summary>
    Vector3 SpawnPosition(ISize item)
    {
        for (int i = 0; i < 10; ++i)
        {
            float x_pos = Random.Range(-1.0f, 1.0f) * waveSpawnXOffset;
            if (x_pos + (item.GetWidth() / 2) < ScreenUtils.ScreenRight && x_pos - (item.GetWidth() / 2) > ScreenUtils.ScreenLeft)
            {
                return new Vector3(x_pos, 0, ScreenUtils.ScreenTop + item.GetHeight());
            }
        }
        return Vector3.zero;
    }
}
