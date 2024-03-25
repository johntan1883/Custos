using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }

        }
    }
    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Player.SetActive(false);
        Time.timeScale = 0.0f;
        Paused = true;
    }
    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Player.SetActive(true);
        Time.timeScale = 1f;
        Paused = false;

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Should Quit the game");

    }
    public void Resume()
    {
        PauseMenuCanvas.SetActive(false);
        Player.SetActive(true);
        Time.timeScale = 1f;
        Paused = false;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1 Demo");
    }
}
