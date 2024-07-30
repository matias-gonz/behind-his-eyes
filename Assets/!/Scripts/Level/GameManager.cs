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
    private string _nextScene;
    private CursorLockMode _cursorLockMode;

    public bool targetHit = false;
    
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

    private IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextScene);
        while (!op.isDone)
        {
            yield return null;
        }

        if (_checkpoint != null && _nextScene == "StreetLevel")
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = _checkpoint.Value.position;
            player.transform.rotation = _checkpoint.Value.rotation;
            GameObject initialSoldier = GameObject.FindWithTag("InitialSoldier");
            SmokingSoldier s = initialSoldier.GetComponent<SmokingSoldier>();
            s.SkipExecutionAnimation();
        }

        Cursor.lockState = _cursorLockMode;
    }

    public void GameOver()
    {
        if (godMode) return;

        LoadScene(Scene.GameOver);
    }

    public void EndFinished()
    {
        LoadScene(Scene.EndCards);
    }

    public void LoadScene(Scene scene)
    {
        switch (scene)
        {
            case Scene.MainMenu:
                _nextScene = "MainMenu";
                _cursorLockMode = CursorLockMode.None;
                break;
            case Scene.StreetLevel:
                _nextScene = "StreetLevel";
                _cursorLockMode = CursorLockMode.Locked;
                break;
            case Scene.Tutorial:
                _nextScene = "Tutorial";
                _cursorLockMode = CursorLockMode.Locked;
                break;
            case Scene.StoryCards:
                _nextScene = "StoryCards";
                _cursorLockMode = CursorLockMode.Locked;
                break;
            case Scene.TitleCards:
                _nextScene = "TitleCards";
                _cursorLockMode = CursorLockMode.Locked;
                break;
            case Scene.EndCards:
                _nextScene = "EndCards";
                _cursorLockMode = CursorLockMode.Locked;
                break;
            case Scene.End:
                _nextScene = "End";
                _cursorLockMode = CursorLockMode.Locked;
                break;
            case Scene.GameOver:
                _nextScene = "GameOver";
                _cursorLockMode = CursorLockMode.None;
                break;
        }

        StartCoroutine(nameof(LoadScene));
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        _checkpoint = checkpoint;
    }
    
    public void TargetHit()
    {
        targetHit = true;
    }
}
