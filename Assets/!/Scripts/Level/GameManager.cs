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

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver()
    {
        if (godMode) return;

        SceneManager.LoadScene("GameOver");
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadScene(Scene scene)
    {
        switch (scene)
        {
            case Scene.StreetLevel:
                _nextScene = "StreetLevel";
                break;
            case Scene.Tutorial:
                _nextScene = "TutorialReal";
                break;
            case Scene.StoryCards:
                _nextScene = "StoryCards";
                break;
            case Scene.TitleCards:
                _nextScene = "TitleCards";
                break;
            case Scene.End:
                _nextScene = "End";
                break;
        }

        StartCoroutine(nameof(LoadScene));
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        _checkpoint = checkpoint;
    }
}
