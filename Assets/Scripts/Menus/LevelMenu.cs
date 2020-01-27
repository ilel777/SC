using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform viewPortContent = GameObject.Find("Viewport").transform.Find("Content");
        for (int i = 0; i < 10; i++)
        {
            GameObject button = Instantiate(Resources.Load<GameObject>("Prefabs/Level Button"), viewPortContent);
            button.GetComponentInChildren<Text>().text = "Level " + (i + 1);
            int n = i;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(n));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadLevel(int i)
    {
        ConfigurationUtils.SetCurrentLevel(i);
        SceneManager.LoadScene("Scene0");
    }
}
