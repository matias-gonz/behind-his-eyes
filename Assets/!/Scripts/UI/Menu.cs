using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using utils;

public class Menu : MonoBehaviour
{
    private Button _self;
    
    private void Start()
    {
        _self = gameObject.GetComponent<Button>();
    }
    
    public void RestartGame()
    {
        _self.interactable = false;
        GameManager.Instance.LoadScene(Scene.StreetLevel);
    }
    
    public void StartGame()
    {
        _self.interactable = false;
        GameManager.Instance.LoadScene(Scene.TitleCards);
    }
}
