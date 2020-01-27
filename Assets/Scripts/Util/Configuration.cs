using UnityEngine;

public class Configuration : MonoBehaviour
{
    [SerializeField]
    private ConfigurationData _configurationData;

    public ConfigurationData ConfigurationData { get => _configurationData; set => _configurationData = value; }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(this);
        _configurationData = ConfigurationData.getConfigurationData();
    }
}
