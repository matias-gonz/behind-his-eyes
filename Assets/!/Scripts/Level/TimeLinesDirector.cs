using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public enum TimeLine
{
    CrawlDialogue,
    FriendlyDialogue,
    GunRangeEnding,
    StreetComplete
}

public class TimeLinesDirector : MonoBehaviour
{
    [SerializeField] private PlayableDirector crawlTLDirector;

    [SerializeField] private PlayableDirector friendlyTLDirector;

    [SerializeField] private PlayableDirector GunRangeEndingTLDirector;

    [SerializeField] private PlayableDirector StreetCompleteTLDirector;

    

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
            case TimeLine.StreetComplete:
                StreetCompleteTLDirector.Play();
                break;                                
        }
    }
}
