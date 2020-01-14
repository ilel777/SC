using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
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


    public void HandleResumeButtonClickEvent()
    {
        Destroy(gameObject);
    }

    public void HandleRestartButtonClickEvent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HandleToMainMenuButtonClickEvent()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
