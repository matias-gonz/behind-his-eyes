using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene = utils.Scene;

public class CallGameManagerTL : MonoBehaviour
{
    public void TutorialFinished()
    {
        GameManager.Instance.LoadScene(Scene.StoryCards);
    }

    public void StreetFinished()
    {
        GameManager.Instance.LoadScene(Scene.End);
    }

    public void EndFinished()
    {
        GameManager.Instance.LoadScene(Scene.EndCards);
    }
}
