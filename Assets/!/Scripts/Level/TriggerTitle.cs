using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTitle : MonoBehaviour
{
    public string titleId;
    public TitleController titleController;
    public VoiceLinesManager voiceLinesManager;
    private bool _triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        
        _triggered = true;
        titleController.ShowTitle(titleId);
        voiceLinesManager.PlayNextLine();
    }
}
