using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    float waveSpawnZ, waveSpawnXOffset;
    List<GameObject> spaceObjects = new List<GameObject>();

    // stores the max width and height of objects to be spawned
    float _maxWidth, _maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawnZ = ScreenUtils.ScreenTop + (ScreenUtils.ScreenTop / 5);
        waveSpawnXOffset = ScreenUtils.ScreenRight;
        spaceObjects.AddRange(Resources.LoadAll<GameObject>("Prefabs/SpaceItems"));

        foreach (GameObject prefab in spaceObjects)
        {
            GameObject obj = Instantiate(prefab);
            float width = obj.GetComponent<ISize>().GetWidth();
            float height = obj.GetComponent<ISize>().GetHeight();
            Destroy(obj);

            if (_maxWidth < width)
            {
                _maxWidth = width;
            }
            if (_maxHeight < height)
            {
                _maxHeight = height;
            }
        }
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
        Vector3 itemSpawnPostition = SpawnPosition(item.GetComponent<ISize>());
        if (itemSpawnPostition != Vector3.zero)
            Instantiate(item, itemSpawnPostition, Quaternion.identity);
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
