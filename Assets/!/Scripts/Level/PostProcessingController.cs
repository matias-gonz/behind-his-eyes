using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingController : MonoBehaviour
{
    [SerializeField] private Volume volume1;
    [SerializeField] private Volume volume2;
    
    public void swapActiveVolume()
    {
        if (volume1.enabled)
        {
            volume2.enabled = true;
            volume1.enabled = false;
        }
        else
        {
            volume2.enabled = false;
            volume1.enabled = true;
        }
    }
}
