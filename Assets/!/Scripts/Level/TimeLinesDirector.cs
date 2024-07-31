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
    StreetComplete,
    PrisonersDialoge
}

public class TimeLinesDirector : MonoBehaviour
{
    [SerializeField] private PlayableDirector crawlTLDirector;

    [SerializeField] private PlayableDirector friendlyTLDirector;

    [SerializeField] private PlayableDirector GunRangeEndingTLDirector;

    [SerializeField] private PlayableDirector StreetCompleteTLDirector;

    [SerializeField] private PlayableDirector prisonersTLDirector;

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
            case TimeLine.PrisonersDialoge:
                prisonersTLDirector.Play();
                break;
        }
    }
}
