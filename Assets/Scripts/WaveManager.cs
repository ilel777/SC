using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    float waveSpawnZ, waveSpawnXOffset;
    List<GameObject> spaceObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        waveSpawnZ = 7.0f;
        waveSpawnXOffset = 2.0f;
        spaceObjects.AddRange(Resources.LoadAll<GameObject>("Prefabs/SpaceItems"));
        InvokeRepeating("SpawnWave", 2.0f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnWave()
    {
        SpawnItem(spaceObjects[Random.Range(0, 3)]);
    }

    void SpawnItem(GameObject item)
    {
        Vector3 itemSpawnPostition = new Vector3(Random.Range(-1.0f, 1.0f) * waveSpawnXOffset, 0, waveSpawnZ);
        Instantiate(item, itemSpawnPostition, Quaternion.identity);
    }
}
