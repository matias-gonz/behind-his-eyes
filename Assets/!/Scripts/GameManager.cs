using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = utils.Scene;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool godMode = false;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StreetLevel");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver()
    {
        if (godMode) return;

        SceneManager.LoadScene("GameOver");
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadScene(Scene nextScene)
    {
        if (nextScene == Scene.StreetLevel)
        {
            RestartGame();
        }
    }
}
