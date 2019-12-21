using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Component waveManager;

    void Awake()
    {
        gameObject.AddComponent<GameInitializer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waveManager = gameObject.AddComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
