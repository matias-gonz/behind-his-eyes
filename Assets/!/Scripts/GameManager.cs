using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = utils.Scene;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool godMode = false;
    
    private Checkpoint? _checkpoint = null;
    private GameObject _player;

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
    
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StreetLevel");
        Debug.Log(_checkpoint);
        if (_checkpoint != null && _player)
        {
            _player.transform.position = _checkpoint.Value.position;
            _player.transform.rotation = _checkpoint.Value.rotation;
        }
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

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        _checkpoint = checkpoint;
    }
}
