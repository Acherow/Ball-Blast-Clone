using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject menu;

    private void Start()
    {
        menu = transform.GetChild(0).gameObject;
        Resume();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
                Pause();
            else
                Resume();
        }
    }

    public void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ToMain()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
