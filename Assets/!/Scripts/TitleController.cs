using System;
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

    private void Start()
    {
        foreach (Title title in titles)
        {
            title.title.SetActive(false);
        }

        ShowTitle("look-around");
        StartCoroutine(nameof(TriggerWalkTitle));
    }

    public void ShowTitle(string titleId)
    {
        Title currentTitle = Array.Find(titles, t => t.title.activeSelf);
        if (currentTitle.title != null)
        {
            currentTitle.title.SetActive(false);
        }

        Title titleToPlay = Array.Find(titles, t => t.id == titleId);

        if (titleToPlay.title == null)
        {
            Debug.LogWarning("Title not found: " + titleId);
            return;
        }

        titleToPlay.title.SetActive(true);
    }

    IEnumerator TriggerWalkTitle()
    {
        yield return new WaitForSeconds(4);
        Title currentTitle = Array.Find(titles, t => t.title.activeSelf);
        if (currentTitle.id == "look-around")
        {
            ShowTitle("walk");
        }
    }
}
