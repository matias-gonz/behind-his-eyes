using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

[System.Serializable]
public struct VoiceLine
{
    public float duration;
    public Audio clip;
    public string subtitle;
}

public class VoiceLinesManager : MonoBehaviour
{
    public PlayerAudio playerAudio;
    public VoiceLine[] voiceLines;
    public SubtitlesScript subtitles;

    private int _currentLine = 0;

    public void PlayNextLine()
    {
        if (_currentLine >= voiceLines.Length)
        {
            Debug.LogWarning("No more voice lines to play");
            return;
        }

        playerAudio.PlayAudio(voiceLines[_currentLine].clip, 1);
        subtitles.ShowSubtitle(voiceLines[_currentLine].subtitle, voiceLines[_currentLine].duration);
        _currentLine++;
    }
}
