using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = utils.Scene;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.LoadScene(Scene.StreetLevel);
    }
}
