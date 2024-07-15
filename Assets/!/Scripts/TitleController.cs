using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Title
{
    public string id;
    public GameObject title;
}

public class TitleController : MonoBehaviour
{
    public Title[] titles;
    
    public void ShowTitle(string titleId)
    {
        Title currentTitle = System.Array.Find(titles, t => t.title.activeSelf);
        if (currentTitle.title != null)
        {
            currentTitle.title.SetActive(false);
        }
        
        Title titleToPlay = System.Array.Find(titles, t => t.id == titleId);

        if (titleToPlay.title == null)
        {
            UnityEngine.Debug.LogWarning("Title not found: " + titleId);
            return;
        }

        titleToPlay.title.SetActive(true);
    }
}
