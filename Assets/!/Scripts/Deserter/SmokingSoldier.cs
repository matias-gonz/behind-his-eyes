using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingSoldier : MonoBehaviour
{
    public GameObject lonelyDeserter;
    private Deserter _deserterScript;

    // Start is called before the first frame update
    void Start()
    {
        _deserterScript = lonelyDeserter.GetComponent<Deserter>();
    }


    private void KillDeserter()
    {
        AudioManager.Instance.PlaySoundFx("k98");
        _deserterScript.GettingKilled();
    }
}
