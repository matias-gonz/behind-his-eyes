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
    PrisonersDialoge,
    TutorialOutro,
    PlayerShot,
    PlayerNoShot
}

public class TimeLinesDirector : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector crawlTLDirector;

    [SerializeField]
    private PlayableDirector friendlyTLDirector;

    [SerializeField]
    private PlayableDirector GunRangeEndingTLDirector;

    [SerializeField]
    private PlayableDirector StreetCompleteTLDirector;

    [SerializeField]
    private PlayableDirector prisonersTLDirector;

    [SerializeField]
    private PlayableDirector tutorialOutroTLDirector;

    [SerializeField]
    private PlayableDirector PlayerShotTLDirector;

    [SerializeField]
    private PlayableDirector PlayerNoShotTLDirector;

    public void PlayTimeLine(TimeLine timeLine)
    {
        Debug.Log(timeLine);
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
            case TimeLine.TutorialOutro:
                tutorialOutroTLDirector.Play();
                break;
            case TimeLine.PlayerShot:
                PlayerShotTLDirector.Play();
                break;
            case TimeLine.PlayerNoShot:
                PlayerNoShotTLDirector.Play();
                break;
        }
    }

    public void PlayerShot()
    {
        TimeLine playerShotTL;
        if (GameManager.Instance.targetHit)
        {
            playerShotTL = TimeLine.PlayerShot;
        }
        else
        {
            playerShotTL = TimeLine.PlayerNoShot;
        }
        PlayTimeLine(playerShotTL);
    }
}
