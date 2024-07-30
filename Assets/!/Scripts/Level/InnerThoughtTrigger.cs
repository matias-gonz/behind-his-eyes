using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerThoughtTrigger : MonoBehaviour
{
    public VoiceLinesManager voiceLinesManager;
    
    private void OnTriggerEnter(Collider other)
    {
        voiceLinesManager.PlayNextLine();
        Destroy(gameObject);
    }
}
