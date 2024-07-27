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
        StartCoroutine(ShowSubtitleCoroutine(subtitle, duration));
    }

    private IEnumerator ShowSubtitleCoroutine(string subtitle, float duration)
    {
        subtitles.text = subtitle;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
