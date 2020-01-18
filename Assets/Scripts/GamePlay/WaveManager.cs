using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    List<Pool<GameObject>> _obstaclesPools = new List<Pool<GameObject>>();

    float waveSpawnZ, waveSpawnXOffset;

    int _objectSpawned;
    int _waveNumber;
    float _speedScale;

    // stores the max width and height of objects to be spawned
    float _maxWidth, _maxHeight;


    // support waiting after spawning an item
    private float _spawnItemWait;
    // support waiting after spawning a wave of items
    Timer _spawnWaveWait;
    // support waiting at the start of level before spawning items
    Timer _startWait;


    // Start is called before the first frame update
    void Start()
    {
        waveSpawnZ = ScreenUtils.ScreenTop + (ScreenUtils.ScreenTop / 5);
        waveSpawnXOffset = ScreenUtils.ScreenRight;

        // spaceObjects.AddRange(Resources.LoadAll<GameObject>("Prefabs/SpaceItems"));
        _obstaclesPools = new List<Pool<GameObject>>();
        _obstaclesPools.Add(PoolsContainer.Enemies);
        _obstaclesPools.Add(PoolsContainer.Asteroids);
        _obstaclesPools.Add(PoolsContainer.Powerups);

        foreach (Pool<GameObject> pool in _obstaclesPools)
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
        // InvokeRepeating("SpawnWave", 1.0f, 1.0f);

        _spawnItemWait = 1.0f;
        _startWait = gameObject.AddComponent<Timer>();
        _startWait.Duration = 0.5f;
        _spawnWaveWait = gameObject.AddComponent<Timer>();
        _spawnWaveWait.Duration = 1.5f;

        _startWait.AddTimerFinishedEventListener(StartSpawnWave);
        _spawnWaveWait.AddTimerFinishedEventListener(StartSpawnWave);

        _startWait.Run();
    }

    private void StartSpawnWave()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        int itemsNumber = 5;

        while (itemsNumber > 0)
        {
            // spawn item
            if (SpawnItem()) itemsNumber--;

            // wait
            if (itemsNumber != 0)
                yield return new WaitForSeconds(_spawnItemWait);
        }

        _spawnWaveWait.Run();
        yield return null;
    }


    GameObject SpawnItem()
    {
        Pool<GameObject> pool = _obstaclesPools[UnityEngine.Random.Range(0, _obstaclesPools.Count)];
        GameObject item = pool.Get();

        // Movement movement = item.GetComponent<Movement>();
        // movement.Speed += (movement.Speed * _speedScale);

        Vector3 itemSpawnPostition = SpawnPosition(item.GetComponent<ISize>());
        if (itemSpawnPostition != Vector3.zero)
        {
            item.transform.position = itemSpawnPostition;
            return item;
        }
        else
        {
            pool.Return(item);
            return null;
        }
        // Instantiate(item, itemSpawnPostition, Quaternion.identity);

    }

    /// <summary>
    ///   generate a random position inside left and right boundaries of the screen
    /// </summary>
    Vector3 SpawnPosition(ISize item)
    {
        for (int i = 0; i < 10; ++i)
        {
            float x_pos = UnityEngine.Random.Range(-1.0f, 1.0f) * waveSpawnXOffset;
            if (x_pos + (item.GetWidth() / 2) < ScreenUtils.ScreenRight && x_pos - (item.GetWidth() / 2) > ScreenUtils.ScreenLeft)
            {
                return new Vector3(x_pos, 0, ScreenUtils.ScreenTop + item.GetHeight());
            }
        }
        return Vector3.zero;
    }

}
