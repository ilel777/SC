using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        ConfigurationUtils.Initialize();
        ScreenUtils.Initialize();
        PoolsContainer.Initialize();
    }
}
