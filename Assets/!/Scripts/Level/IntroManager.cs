using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public TitleController titleController;

    public void EndIntroCinematic()
    {
        titleController.StartTitles();
    }
}
