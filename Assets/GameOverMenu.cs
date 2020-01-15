using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }


    public void HandleRestartButtonClickEvent()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
        Destroy(gameObject);
    }

    public void HandleToMainMenuButtonClickEvent()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
