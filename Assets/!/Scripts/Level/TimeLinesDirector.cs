using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public enum TimeLine
{
    CrawlDialogue,
    FriendlyDialogue
}

public class TimeLinesDirector : MonoBehaviour
{
    [SerializeField] private PlayableDirector crawlTLDirector;

    [SerializeField] private PlayableDirector friendlyTLDirector;

    public void PlayTimeLine(TimeLine timeLine)
    {
        switch (timeLine)
        {
            case TimeLine.CrawlDialogue:
                crawlTLDirector.Play();
                break;
            case TimeLine.FriendlyDialogue:
                friendlyTLDirector.Play();
                break;
        }
    }
}
