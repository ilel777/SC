using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    float waveSpawnZ, waveSpawnXOffset;
    [SerializeField]
    GameObject asteroid;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawnZ = 7.0f;
        asteroid = Resources.Load("Prefabs/asteroid") as GameObject;
        InvokeRepeating("SpawnWave", 2.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnWave(){
        SpawnItem(asteroid);
    }

    void SpawnItem(GameObject item){
        Vector3 itemSpawnPostition = new Vector3(Random.Range(-1,1), 0, waveSpawnZ);
        Instantiate(item, itemSpawnPostition, Quaternion.identity);
    }
}
