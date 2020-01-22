using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    List<Pool<GameObject>> _obstaclesPools = new List<Pool<GameObject>>();

    float waveSpawnZ, waveSpawnXOffset;

    int _objectSpawned;
    int _waveCount;
    float _speedScale;

    // stores the max width and height of objects to be spawned
    float _maxWidth, _maxHeight;


    // support waiting after spawning an item
    private float _spawnItemWait;
    // support waiting after spawning a wave of items
    Timer _spawnWaveWait;
    // support waiting at the start of level before spawning items
    Timer _startWait;
    private GameObject _msg;

    // support spawn Message
    float _spawnMessageDelay;


    // Start is called before the first frame update
    void Start()
    {
        waveSpawnZ = ScreenUtils.ScreenTop + (ScreenUtils.ScreenTop / 5);
        waveSpawnXOffset = ScreenUtils.ScreenRight;

        // spaceObjects.AddRange(Resources.LoadAll<GameObject>("Prefabs/SpaceItems"));
        _obstaclesPools = new List<Pool<GameObject>>();
        // _obstaclesPools.Add(PoolsContainer.Enemies);
        foreach (string name in PoolsContainer.SpaceShipPools.Keys)
        {
            if (name != "Player")
                _obstaclesPools.Add(PoolsContainer.SpaceShipPools[name]);
        }
        _obstaclesPools.AddRange(PoolsContainer.ObstaclePools.Values);
        _obstaclesPools.AddRange(PoolsContainer.PowerupPools.Values);

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

        _msg = Instantiate(Resources.Load<GameObject>("Prefabs/Spawn Message"));
        _msg.SetActive(false);

        _spawnItemWait = ConfigurationUtils.WaveConfig.spawnItemWait;
        _spawnMessageDelay = ConfigurationUtils.WaveConfig.spawnMessageDelay;

        _startWait = gameObject.AddComponent<Timer>();
        _startWait.Duration = ConfigurationUtils.WaveConfig.startWait;
        _spawnWaveWait = gameObject.AddComponent<Timer>();
        _spawnWaveWait.Duration = ConfigurationUtils.WaveConfig.spawnWaveWait;

        _startWait.AddTimerFinishedEventListener(StartSpawnWave);
        _spawnWaveWait.AddTimerFinishedEventListener(StartSpawnWave);

        _startWait.Run();
    }

    private void StartSpawnWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator ShowSpawnMessage()
    {
        _msg.SetActive(true);
        _msg.GetComponentInChildren<Text>().text = "Wave " + (_waveCount + 1);
        yield return new WaitForSeconds(_spawnMessageDelay);
        _msg.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        int itemsNumber = ConfigurationUtils.WaveConfig.itemsNumber;
        yield return StartCoroutine(ShowSpawnMessage());

        while (itemsNumber > 0)
        {
            // spawn item
            if (SpawnItem()) itemsNumber--;

            // wait
            if (itemsNumber != 0)
                yield return new WaitForSeconds(_spawnItemWait);
        }

        _spawnWaveWait.Run();
        _waveCount++;
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
