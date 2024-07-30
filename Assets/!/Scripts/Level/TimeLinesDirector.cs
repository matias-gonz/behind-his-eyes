using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public enum TimeLine
{
    CrawlDialogue,
    FriendlyDialogue,
    GunRangeEnding
}

public class TimeLinesDirector : MonoBehaviour
{
    [SerializeField] private PlayableDirector crawlTLDirector;

    [SerializeField] private PlayableDirector friendlyTLDirector;

    [SerializeField] private PlayableDirector GunRangeEndingTLDirector;

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
            case TimeLine.GunRangeEnding:
                GunRangeEndingTLDirector.Play();
                break;                
        }
    }
}
