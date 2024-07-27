using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitlesScript : MonoBehaviour
{
    public TextMeshProUGUI subtitles;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowSubtitle(string subtitle, float duration)
    {
        subtitles.text = subtitle;
        gameObject.SetActive(true);
        StartCoroutine(SubtitleDelay(duration));
    }

    private IEnumerator SubtitleDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
