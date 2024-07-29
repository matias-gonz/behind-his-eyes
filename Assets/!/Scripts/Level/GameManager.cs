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
    
    private IEnumerator RestartGame()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("StreetLevel");
        while (!op.isDone)
        {
            yield return null;
        }
        if (_checkpoint != null)
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

    private IEnumerator StartTutorial()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("TutorialReal");
        while (!op.isDone)
        {
            yield return null;
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
                StartCoroutine(nameof(RestartGame));
                break;
            case Scene.Tutorial:
                StartCoroutine(nameof(StartTutorial));
                break;
        }
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        _checkpoint = checkpoint;
    }
}
