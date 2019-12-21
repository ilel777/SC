using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Component waveManager;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<GameInitializer>();
        waveManager = gameObject.AddComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
