using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene = utils.Scene;

public class GameOverMenu : MonoBehaviour
{
    public GameObject restartButton;
    private Button _restartButton;
    
    private void Start()
    {
        _restartButton = restartButton.GetComponent<Button>();
    }
    
    public void RestartGame()
    {
        _restartButton.interactable = false;
        GameManager.Instance.LoadScene(Scene.StreetLevel);
    }
}
