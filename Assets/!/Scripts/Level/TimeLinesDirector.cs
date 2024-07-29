using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLinesDirector : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector crawlTLDirector;

    [SerializeField]
    private PlayableDirector friendlyTLDirector;

    void Start() { }

    public void PlayCrawlDialogeTimeLine()
    {
        crawlTLDirector.Play();
    }

    public void PlayFriendlyDialogeTimeLine()
    {
        friendlyTLDirector.Play();
    }
}
