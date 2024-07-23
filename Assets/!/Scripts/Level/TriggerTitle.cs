using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTitle : MonoBehaviour
{
    public string titleId;
    public TitleController titleController;

    private void OnTriggerEnter(Collider other)
    {
        titleController.ShowTitle(titleId);
    }
}
