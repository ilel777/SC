using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Support wave management
    WaveManager waveManager;

    // Support level management
    LevelManager levelManager;


    void Awake()
    {
        gameObject.AddComponent<GameInitializer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waveManager = gameObject.AddComponent<WaveManager>();
        levelManager = gameObject.AddComponent(Type.GetType(ConfigurationUtils.LevelConfig.name)) as LevelManager;
        levelManager = GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        EventManager.StartListening(EventName.GameOver, HandleGameOverEvent);
        EventManager.StartListening(EventName.MissionComplete, HandleMissionCompleteEvent);
    }

    private void HandleMissionCompleteEvent(EventArgs arg0)
    {
        // Debug.Log("Mission Complete");
    }

    /// <summary>
    ///   just print "game over" on console
    /// </summary>
    private void HandleGameOverEvent(EventArgs arg0)
    {
        // Debug.Log("Game Over");
    }

    void OnDisable()
    {
        EventManager.StopListening(EventName.GameOver, HandleGameOverEvent);
        EventManager.StopListening(EventName.MissionComplete, HandleMissionCompleteEvent);
    }
}
